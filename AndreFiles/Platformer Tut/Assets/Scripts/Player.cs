using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;

	float accelerationTimeAirBorn = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 15;
	float gravity = -30;
	float jumpVelocity;
	
	float velocityXSmoothing;
	
	Vector3 velocity;
	Controller2D controller;
	
	void Start () {
		controller = GetComponent<Controller2D> ();
		
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	void Update() {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		
		/*
		*	todo FIX:  --> When moves on the slope down or up can not jump!!!!
		*/
		
		if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}
		
		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded: accelerationTimeAirBorn));
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
