using UnityEngine;
using System.Collections;

public class Controller2D : RayCastController {
	
	float maxClimbAngle = 80;
	float maxDecendAngle = 75;

	public CollisionsInfo collisions;
	
	// virtual to the base start method and override to the one thats going to run it and itself
	public override void Start() {
		base.Start ();
		
	}
	
	public void Move(Vector3 velocity) {
		UpdateRayCastOrigins();
		collisions.Reset();
		
		collisions.velocityOld = velocity;
		
		if(velocity.y < 0) {
			DecendSlope(ref velocity);
		}
		
		if (velocity.x != 0) {
			HorizontalCollisions( ref velocity); 
		}
		
		if(velocity.y != 0 ) {
			VerticalCollisions (ref velocity);
		}
		
		transform.Translate(velocity);
	}
	
	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;
	
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
			
			if(hit) {

				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}
		
		if(collisions.climbingSlope) {
			float directionX = Mathf.Sign(velocity.x);
			rayLength = Mathf.Abs(velocity.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft:raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			
			if(hit) {
				float slopeAngel = Vector2.Angle(hit.normal, Vector2.up);
				if(slopeAngel != collisions.slopeAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngel;
				}
			}
		}
	}
	
	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green);
			
			if(hit) {
			
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if(i == 0 && slopeAngle <= maxClimbAngle) {
				
					if (collisions.decendingSlope) {
						collisions.decendingSlope = false;
						velocity = collisions.velocityOld;
					}
					
					float distanceToSlopeStart = 0;
					if(slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance - skinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}
					
					ClimbSlope(ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * directionX;
				}
				
				if(!collisions.climbingSlope || slopeAngle > maxClimbAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;
					
					if(collisions.climbingSlope) {
						velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
					}
					
					collisions.left = directionX == -1;
					collisions.right = directionX == 1;
				}
			}
		}
	}
	
	void ClimbSlope(ref Vector3 velocity, float slopeAngel){
		float moveDistance = Mathf.Abs(velocity.x);
		
		float climbVelocityY = Mathf.Sin(slopeAngel * Mathf.Deg2Rad) * moveDistance;
		if(velocity.y <= climbVelocityY) {
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos(slopeAngel * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngel;
		}
		
	}
	
	void DecendSlope (ref Vector3 velocity) {
		float directionX = Mathf.Sign(velocity.x);
		Vector2 rayOrigin = (directionX == -1)? raycastOrigins.bottomRight: raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
		
		if(hit) {
			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if( slopeAngle != 0 && slopeAngle <= maxDecendAngle) {
				if(Mathf.Sign(hit.normal.x) == directionX) {
					if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)) {
						float movedistance = Mathf.Abs(velocity.x);
						float decendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * movedistance;
						velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * movedistance * Mathf.Sign(velocity.x);
						velocity.y -= decendVelocityY;
						
						collisions.slopeAngle = slopeAngle;
						collisions.decendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}
	
	public struct CollisionsInfo
	{
		public bool above, below;
		public bool left, right;
		
		public bool climbingSlope;
		public float slopeAngle, slopeAngleOld;
		public bool decendingSlope;
		public Vector3 velocityOld;
		
		
		public void Reset() {
			above = below = false;
			left = right = false;
			climbingSlope = false;
			decendingSlope = false;
			
			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}
}

