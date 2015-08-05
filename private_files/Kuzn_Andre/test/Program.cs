using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics

namespace test
{
    public class Program
    {
        public void Main(string[] args)
        {
        	Console.WriteLine("Hello World");

    		// play with variables
            // declaration
			int num = 452147483648;
			sbyte smallnum = 126;

			ulong biggestPossible = 184467440737039;
			// the ulong is the longest one

			sbyte a = 80;
			sbyte s = a + 10;

			int result = a + s;
			bool test = true;

			float temp;
			string name = "Andre";

			char firstLetter = "A";
			int[] source = (3,4,5,6,2,1,34,3);

			var limit = 5;
			var query = from item in source where item <= limit select item;

			foreach (int s in source) {
				Console.WriteLine(s);
			}

			@"the A staff before the string is VERBATUM to escape anything special in the string
			also the spaces
			as many as you like"
        }
    }
}



// accessing the text and modifing it
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textController : MonoBehaviour {

	public Text text;
	void Start () {
		text.text = "Text to display on the screen!!"
	}

	void Update () {
		if (Input.GetKeyDown("space")) {
			text.text = "This is the changed version of the text on space press";
		}
	}
}


// completed game of choice with different states to be changed 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	public Text text;
	private enum States {cell, sheet_0, sheet_1, lock_0, lock_1, mirror, cell_mirror, freedom};
	private States myState;

	void Start() {
		myState = States.cell;
	}

	void Update() {
		print (myState);
		switch (myState) {
			case States.cell:
				state_cell();
				break;
			case States.sheet_0:
				state_sheet_0();
				break;
			case States.sheet_1:
				state_sheet_1();
				break;
			case States.lock_1:
				state_lock_1();
				break;
			case States.lock_0:
				state_lock_0();
				break;
			case States.mirror:
				state_mirror();
				break;
		}
	}

	void state_cell() {
		text.text = "Here is the beging of the game \n" +
					"press s to view sheets, m to see the mirror and l to view the lock";
		if (Input.GetKeyDown(KeyCode.S)) {
			myState = States.sheet_0;
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			myState = States.mirror;
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			myState = States.lock_0;
		}
	} 

	void state_sheet_0() {
		text.text = "You are next to the sheets \n" +
					"press n to view further, m to see the mirror and l to view the lock, " +
					"and c to get back where you where before";
		if (Input.GetKeyDown(KeyCode.N)) {
			myState = States.sheet_1;
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			myState = States.mirror;
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			myState = States.lock_0;
		}
		else if (Input.GetKeyDown(KeyCode.C)) {
			myState = States.cell;
		}
	} 


	void state_sheet_1() {
		text.text = "You are next to the sheets \n" +
					"press s to get back to old sheets, m to see the mirror and l to view the lock, " +
					"and c to get back where you where before";
		if (Input.GetKeyDown(KeyCode.S)) {
			myState = States.sheet_0;
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			myState = States.mirror;
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			myState = States.lock_0;
		}
		else if (Input.GetKeyDown(KeyCode.C)) {
			myState = States.cell;
		}
	} 


	void state_lock_0() {
		text.text = "You are next to the cell lock \n" +
					"press s to get back to old sheets, m to see the mirror, " +
					"and c to get back where you where before \n" + 
					" press n to look closer at the lock";
		if (Input.GetKeyDown(KeyCode.S)) {
			myState = States.sheet_0;
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			myState = States.mirror;
		}
		else if (Input.GetKeyDown(KeyCode.N)) {
			myState = States.lock_1;
		}
		else if (Input.GetKeyDown(KeyCode.C)) {
			myState = States.cell;
		}
	} 

	void state_lock_1() {
		text.text = "You are next to the cell lock \n" +
					"press s to get back to old sheets, m to see the mirror, " +
					"and c to get back where you where before, press l to go to old lock";

		if (Input.GetKeyDown(KeyCode.S)) {
			myState = States.sheet_0;
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			myState = States.mirror;
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			myState = States.lock_0;
		}
		else if (Input.GetKeyDown(KeyCode.C)) {
			myState = States.cell;
		}
	} 

	void state_lock_1() {
		text.text = "Well you are infront of the mirror, look pretty shitty today...";

		if (Input.GetKeyDown(KeyCode.S)) {
			myState = States.sheet_0;
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			myState = States.lock_0;
		}
		else if (Input.GetKeyDown(KeyCode.C)) {
			myState = States.cell;
		}
	} 

}


// logic for the number game
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

	int max;
	int min;
	int guess;
	int compGuess = 10;
	
	public Text foo;

	// Use this for initialization
	void Start () {
		StartGame();
	}
	
	void StartGame () {
		max = 1001;
		min = 1;
		NextGuess();
	}
	
	public void GuessLower() {
		max = guess;
		NextGuess();
	}
	
	public void GuessHigher() {
		min = guess;
		NextGuess();
	}
	
	void NextGuess () {
		guess = Random.Range(min, max+1);
		compGuess = compGuess - 1;
		foo.text = guess.ToString();
		
		if(compGuess <= 0) {
			Application.LoadLevel("Win");
		}
	}
}








