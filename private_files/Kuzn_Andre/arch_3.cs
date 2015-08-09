using UnityEngine;
using System.Collections;

// script to change the color of the cube on keypress
public class ExampleBehaviourSrc : MonoBehaviour
{
	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.R)) 
		{
			gameObject.renderer.material.color = Color.red;
		}
		else if (Input.GetKeyDown(KeyCode.G)) 
		{
			gameObject.renderer.material.color = Color.green;
		}
		else if (Input.GetKeyDown(KeyCode.B)) 
		{
			gameObject.renderer.material.color = Color.blue;
		}
	}
}

public class ExampleVariableTut : MonoBehaviour
{
	int myInt = 56;

	void Start() 
	{
		myInt = 12;
		// can use Console.ReadLine() to supply the params for the function
		int staff = MultiplyByTwo(4, 12);

		Console.WriteLine(staff);
		Debug.Log(myInt * 45);
	}

	int MultiplyByTwo (int num, int, secondnum) 
	{
		int ret;
		ret = num * secondnum;
		return ret;
	}

	void static customFunction () 
	{

	}
}

public class ExampleBasicSynatx : MonoBehaviour
{
	/* 
	can find 
		     transform.position.x 
	 		 transform.position.y 
	 */

	 // coffee example game
	 public float cofTemp = 85.0f;
	 public float hotLimitTemp = 70.0f;
	 public float coldLimitTemp = 40.0f;

	 void Update() 
	 {
	 	if(Input.GetKeyDown(KeyCode.Space)) 
	 	{
	 		checkTemperature();
	 	}

 		cofTemp -= Time.deltaTime * 5f;
	 }

	 void checkTemperature() 
	 {
	 	if (cofTemp > hotLimitTemp)
	 	{
	 		Debug.Log("Too Hot");
	 	}
	 	else if ( cofTemp < coldLimitTemp)
	 	{
	 		Debug.Log("Too Cold");
	 	}
	 	else
	 	{
	 		Debug.Log("Just right");
	 	}
	 }

	 int myFunction (int age, string name) {
	 	int staff = age * 56;
 		int[][] arr = new int[][] { new string[] { name, "sup"}, new int[] {staff, 675}};

	 	return arr;
	 }
}

public class ExampleLoopExers : MonoBehaviour
{
	int staffs = 100;
	public void Start() 
	{
		bool sC = false;

		//do while
		do 
		{
			print("staff");

		}while(sC == true);


		// while
		while(staffs > 0 ) 
		{
			staffs--;
			print(staffs);
		}

		// for loop
		for(int i = 0; i < staffs; i++) 
		{
			Debug.Log("Creating stafffs " + i);
		}

		// foreach

		string[] strings = new string[10];

		strings[0] = "First";
		strings[1] = "second";
		strings[2] = "third";
		strings[3] = "forth";
		strings[4] = "fif";
		strings[5] = "sixth";
		strings[6] = "seventh";
		
		foreach(string s in strings) 
		{
			print (s);
		}


		// Stoped on 06. Loops
		// Go to the next one
	}
}


public class ExampleScopeAndAccess : MonoBehaviour
{
	public int alpha = 5;

	private int beta = 0;
	private int gamma = 5;

	//new class instance	
	private ExampleLoopExers myLoopClass;

	//new class instance	
	private AnotherClass myAnotherClass;

	void Start() {
		alpha = 45;

		// use new class instance

		myAnotherClass = new AnotherClass();
		myAnotherClass.FruitMachine(3, 54);

		// use new class instance 
		myLoopClass = new ExampleLoopExers();
		myLoopClass.Start();
	}

	void Example(int a, int b) {

		int ans;
		ans = a * b * alpha;
		Debug.Log(ans);
	}

	void Update() {
		Debug.Log("Alpha is set to: " + alpha);
	}

}

public class AnotherClass 
{
	public int apples;
	public int bananas;

	private int stapler;
	private int sellotape;

	public void FruitMachine(int a, int b) {
		int ans;
		ans = a + b;
		Debug.Log("Fruit total: " + ans);
	}

	private void OfficeSort(int a, int b) {
		int answer;
		answer = a + b;
		Debug.Log("Office supplies total: " + answer);
	}
}


// Main functions of Unity engine
public class MajorParts : MonoBehaviour 
{
	// Awake and Start are called on script initialization, just once
	void Awake() {

		//good for references between scripts, initialisation
		//ex. Enemy appear, get their ammo
	}

	void Start() {

		// runs only when/ if script component is enabled
		//ex. Enemy gets ability to shoot
	}
	// Update called after Awake and Start and called repeatedly every frame
	void Update() {

		// called every frame
		// Used for regular updates
		/*
			-moving non-physics objs
			-simple timers
			-receiving input
		*/
		// UPDATE INTERVAL TIME VARY

		Debug.Log("Update time: " + Time.deltaTime);
	}

	void FixedUpdata() {
		// Called Every Physics Step
		// Fixed Update intervals are consistent
		// Used for regular updates such as :
		//	- Adjusting Physics (Rigidbody) objects

		Debug.Log("FixedUpdate time: " + Time.deltaTime);
	}
}


// super impressive ass science around the physics in Unity
// https://unity3d.com/learn/tutorials/modules/beginner/scripting/vector-maths-dot-cross-products?playlist=17117

/*
	- Vector2
	- Vector3
	- Vector3.Dot(VectorA, VectorB);
	- Vector3.Cross(VectorA, VectorB);

*/


// cool thing attach it to the light and pressing space can switch toggle it
public class ExampleEnAbleingComponents : MonoBehaviour 
{
	private Light myLight;

	void Start() {
		myLight = GetComponent<Light>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			myLight.enabled = !myLight.enabled;
		}
	}
}


// cool thing attach it to the light and pressing space can switch toggle it
public class ExampleGameObjActiveInactive : MonoBehaviour 
{
	public GameObject myObject;

	void Start() {
		// on load will set it inactive
		gameObject.SetActive(false);

		// you can check the state of the game objs with 
		Debug.Log("Active self: " + myObject.activeSelf);
		Debug.Log("Active in hierarchy: " + myObject.activeInHierarchy);
	}
}


// 13. stoped on translate and rotate















