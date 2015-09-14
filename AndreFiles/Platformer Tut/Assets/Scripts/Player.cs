using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;

	float accelerationTimeAirBorn = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 15;
	
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;
	
	float gravity = -30;
	float maxJumpVelocity;
	float minJumpVelocity;
	
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnStick;
	
	
	float velocityXSmoothing;
	
	Vector3 velocity;
	Controller2D controller;
	
	void Start () {
		controller = GetComponent<Controller2D> ();
		
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity)  * minJumpHeight);
	}
	
	void Update() {
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		int wallDirectionX = (controller.collisions.left) ? -1 : 1;
			
		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded: accelerationTimeAirBorn));
		
		bool wallSliding = false;
		
		if((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;
			
			if(velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}
			
			if(timeToWallUnStick > 0) {
				velocity.x = 0;
				velocityXSmoothing = 0;
				
				if(input.x != wallDirectionX && input.x != 0) {
					timeToWallUnStick -= Time.deltaTime;			
				}
				else {
					timeToWallUnStick = wallStickTime;
				}
			}
			else {
				timeToWallUnStick = wallStickTime;
			}
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
			
		/*
		*	todo FIX:  --> When moves on the slope down or up can not jump!!!!
		*/
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(wallSliding) {
				if(wallDirectionX == input.x) {
					velocity.x = -wallDirectionX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if(input.x == 0) {
					velocity.x = -wallDirectionX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else {
					velocity.x = -wallDirectionX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if(controller.collisions.below) {
				velocity.y = maxJumpVelocity;			
			}
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			if(velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;			
			}
		}
		
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
