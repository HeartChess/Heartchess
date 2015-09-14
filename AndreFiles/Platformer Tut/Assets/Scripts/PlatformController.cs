using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : RayCastController {

	public LayerMask passengerMask;
	
	public Vector3[] localwayPoints;
	public Vector3[] globalWayPoints;

	public float speed;
	public bool cyclic;
	public float waitTime;
	
	[Range(0, 2)]
	public float EaseAmount;
	
	int fromWayPointIndex;
	float percentBetweenWayPoints;
	float nextMoveTime;
	
	List<PassengerMovement> passengerMovement;
	Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D> ();

	// Use this for initialization
	public override void Start () {
		base.Start();
		
		globalWayPoints = new Vector3[localwayPoints.Length];
		for (int i = 0; i < localwayPoints.Length; i++) {
			globalWayPoints[i] = localwayPoints[i] + transform.position;
		}
	}
	
	void Update() {
		UpdateRayCastOrigins();
	
		Vector3 velocity = CalculatePlatformMovement();
		
		CalculatePassengerMovement(velocity);
		
		MovePassengers(true);
		transform.Translate (velocity);
		MovePassengers(false);
	}
	
	float Ease (float x) {
		float a = EaseAmount + 1;
		return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1-x, a));
	}
	
	Vector3 CalculatePlatformMovement () {
	
		if(Time.time < nextMoveTime) {
			return Vector3.zero;
		}
	
		fromWayPointIndex %= globalWayPoints.Length;
		int toWayPointIndex = (fromWayPointIndex + 1) % globalWayPoints.Length;
		float distanceBetweenWaypoints = Vector3.Distance(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointIndex]);
		percentBetweenWayPoints += Time.deltaTime * speed/distanceBetweenWaypoints;
		percentBetweenWayPoints = Mathf.Clamp01(percentBetweenWayPoints);
		float easedPersent = Ease(percentBetweenWayPoints);
		
		Vector3 newPosition = Vector3.Lerp(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointIndex], easedPersent);
		
		if(percentBetweenWayPoints >= 1) {
			percentBetweenWayPoints = 0;
			fromWayPointIndex ++;
			
			if(!cyclic) {
				if(fromWayPointIndex >= globalWayPoints.Length - 1) {
					fromWayPointIndex = 0;
					System.Array.Reverse(globalWayPoints);
				}
			}
			nextMoveTime = Time.time + waitTime;
		}
		
		return newPosition - transform.position;
	}
	
	void MovePassengers(bool beforeMovePlatform) {
		foreach (PassengerMovement passenger in passengerMovement) {
			if(!passengerDictionary.ContainsKey(passenger.transform)) {
				passengerDictionary.Add (passenger.transform, passenger.transform.GetComponent<Controller2D>());
			}
			if(passenger.moveBeforePlatform == beforeMovePlatform) {
				passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standOnPlatform);
			}
		}
	}
	
	void CalculatePassengerMovement(Vector3 velocity) {
		HashSet<Transform> movedPassengers = new HashSet<Transform>(); // particulary fast at adding thing and checking if they contain things
		passengerMovement = new List<PassengerMovement>();
	
		float directionX = Mathf.Sign(velocity.x);
		float directionY = Mathf.Sign(velocity.y);
		
		// vertical
		if(velocity.y != 0) {
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i++) {
				Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
				
				if (hit && hit.distance != 0) {
					if(!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1) ? velocity.x : 0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));						
						
					}
				}
				
			}
		}
		
		// horizontally 
		
		if(velocity.x != 0) {
			float rayLength = Mathf.Abs (velocity.x) + skinWidth;
			
			for (int i = 0; i < horizontalRayCount; i++) {
			
				Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);
				
				if (hit && hit.distance != 0) {
					if(!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						float pushY = -skinWidth;
						
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
					}
				}
				
			}
		}
		
		
		// pass on top of hor or vertic downward moving pltform
		if(directionY == -1 || velocity.y == 0 && velocity.x != 0) {
		
			float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i++) {
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
				if (hit && hit.distance != 0) {
					if(!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;
						
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));	
					}
				}
				
			}
			
		}
	}
	
	struct PassengerMovement
	{
		public Transform transform;
		public Vector3 velocity;
		
		public bool standOnPlatform;
		public bool moveBeforePlatform;
		
		public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform) {
			transform = _transform;
			velocity = _velocity;
			standOnPlatform = _standingOnPlatform;
			moveBeforePlatform = _moveBeforePlatform;
		}
	}
	
	
	void OnDrawGizmos () {
		if(localwayPoints != null) {
			Gizmos.color = Color.red;
			float size = .3f;
			for (int i = 0; i < localwayPoints.Length; i++) {
				Vector3 globalWaypointPosition = (Application.isPlaying) ? globalWayPoints[i] : localwayPoints[i] + transform.position;
				
				Gizmos.DrawLine(globalWaypointPosition - Vector3.up * size, globalWaypointPosition + Vector3.up * size);
				Gizmos.DrawLine(globalWaypointPosition - Vector3.left * size, globalWaypointPosition + Vector3.left * size);
				
			}
		}
	}
	
	
	
	
	
	
	
	
}
