using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienMovement : MonoBehaviour {

	public float xVel;
	public GameObject alien;

	private float lowerLevel;
	private bool goingDown;
	private ArrayList alienRows;

	// Use this for initialization
	void Start () {
		goingDown = false;
		lowerLevel = transform.position.y - 0.5f;
		alienRows = new ArrayList ();
		for (int i = 0; i < 5; i++) {
			alienRows.Add (new ArrayList ());
		}
		for(int i = 0; i < alienRows.Count; i++) {
			for (int j = 0; j < 3; j++) {
				GameObject newAlien = Instantiate (alien, new Vector3 (transform.position.x + i * 1.5f, transform.position.y - j, transform.position.z), Quaternion.identity);
				(alienRows[i] as ArrayList).Add(newAlien);
				newAlien.GetComponent<AlienScript> ().setArrayPosition (i, j);
			}
		}
		foreach(ArrayList row in alienRows){
			foreach (GameObject alien in row) {
				alien.transform.parent = this.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!goingDown && (((alienRows[0] as ArrayList)[0] as GameObject).transform.position.x < -8 || ((alienRows[alienRows.Count - 1] as ArrayList)[0] as GameObject).transform.position.x > 8)) {	// use x position of zeroth and last alien row
			xVel *= -1;
			goingDown = true;
			lowerLevel = transform.position.y - 0.5f;
		}
		if (goingDown) {
			transform.Translate(0, -1 * Mathf.Abs(xVel), 0);
			if (transform.position.y <= lowerLevel) {
				goingDown = false;
				transform.Translate(xVel, 0, 0);
			}
		} else {
			transform.Translate(xVel, 0, 0);
		}
	}

	public void removeAlien(int row, int col){
		if (row < alienRows.Count && col < (alienRows [row] as ArrayList).Count) {
			(alienRows [row] as ArrayList).RemoveAt (col);
			for (int i = col; i < (alienRows [row] as ArrayList).Count; i++) {
				AlienScript theAlien = ((alienRows [row] as ArrayList) [i] as GameObject).GetComponent<AlienScript> ();
				theAlien.setArrayPosition (theAlien.getRow (), theAlien.getCol () - 1);
			}
			if ((alienRows [row] as ArrayList).Count == 0) {
				alienRows.RemoveAt (row);
				for (int i = row; i < alienRows.Count; i++) {
					for (int j = 0; j < (alienRows [i] as ArrayList).Count; j++) {
						AlienScript theAlien = ((alienRows [i] as ArrayList) [j] as GameObject).GetComponent<AlienScript> ();
						theAlien.setArrayPosition (theAlien.getRow () - 1, theAlien.getCol ());
					}
				}
			}
		}
		if (alienRows.Count == 0) {
			SceneManager.LoadScene ("Win");
		}
	}
}
