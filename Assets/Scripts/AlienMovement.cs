using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienMovement : MonoBehaviour
{

	public float xVel;
	public GameObject alien;
	public GameObject explosion;

	private string gameMode;
	private ArrayList alienRows;
	private ArrayList rowParents;
	private bool goingDown;
	private Sprite alien1;
	private Sprite alien2;
	private Sprite alien3;

	// Use this for initialization
	void Start ()
	{
		gameMode = GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel ();
		alien1 = Resources.Load<Sprite> ("Art/" + gameMode + "/alien 1");
		alien2 = Resources.Load<Sprite> ("Art/" + gameMode + "/alien 2");
		alien3 = Resources.Load<Sprite> ("Art/" + gameMode + "/alien 3");
		goingDown = false;
		alienRows = new ArrayList ();
		rowParents = new ArrayList ();
		for (int i = 0; i < 7; i++) {
			alienRows.Add (new ArrayList ());
		}
		for (int i = 0; i < alienRows.Count; i++) {
			GameObject rowParent = new GameObject ("RowParent" + i);
			rowParent.transform.position = new Vector3 (transform.position.x + i * 1.5f, transform.position.y, transform.position.z);
			rowParent.transform.parent = this.transform;
			for (int j = 0; j < 3; j++) {
				GameObject newAlien = Instantiate (alien, new Vector3 (transform.position.x + i * 1.5f, transform.position.y - j, transform.position.z), Quaternion.identity);
				(alienRows [i] as ArrayList).Add (newAlien);
				AlienScript theAlien = newAlien.GetComponent<AlienScript> ();
				theAlien.setArrayPosition (i, j);
				switch (j) {
				case 0:
					theAlien.setSprite (alien3);
					break;
				case 1:
					theAlien.setSprite (alien2);
					break;
				case 2:
					theAlien.setSprite (alien1);
					break;
				default:
					break;
				}
				newAlien.transform.parent = rowParent.transform;
				switch (gameMode) {
				case "Classic":
				case "The Net":
					newAlien.GetComponent<Animator> ().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animations/"+gameMode+" alien " + (j + 1) + " controller") as RuntimeAnimatorController;
					break;
				default:
					Destroy (newAlien.GetComponent<Animator> ());
					break;
				}
			}
			rowParents.Add (rowParent);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (alienRows.Count != 0) {
			if (!goingDown) {
				if ((rowParents [0] as GameObject).transform.position.x < -8 || (rowParents [rowParents.Count - 1] as GameObject).transform.position.x > 8) {	// use x position of zeroth and last alien row
					StartCoroutine (goDown (transform.position.y - 0.5f));
				} else {
					transform.Translate (xVel, 0, 0);
				}
			}
		} else if (GameObject.FindGameObjectsWithTag ("explosion").Length == 0) {
			SceneManager.LoadScene ("Win");
		}
	}

	IEnumerator goDown (float lowerLevel)
	{
		goingDown = true;
		xVel *= -1;
		while (transform.position.y > lowerLevel) {
			transform.Translate (0, -1 * Mathf.Abs (xVel), 0);
			yield return null;
		}
		transform.Translate (xVel, 0, 0);
		goingDown = false;
	}

	bool checkRowEmpty (int row)
	{
		bool thisRowEmpty = true;
		foreach (GameObject alien in alienRows[row] as ArrayList) {
			if (alien != null) {
				thisRowEmpty = false;
			}
		}
		return thisRowEmpty;
	}

	public void removeAlien (int row, int col, string expType = "")
	{
		if (row >= 0 && row < alienRows.Count && col >= 0 && col < (alienRows [row] as ArrayList).Count && (alienRows [row] as ArrayList) [col] != null) {
			GameObject exp = Instantiate (explosion, ((alienRows [row] as ArrayList) [col] as GameObject).GetComponent<AlienScript> ().getPos (), Quaternion.identity);
			exp.GetComponent<ExplosionControl> ().setType (expType);
			Destroy ((alienRows [row] as ArrayList) [col] as GameObject);
			(alienRows [row] as ArrayList) [col] = null;
			if (checkRowEmpty (row)) {
				Destroy (rowParents [row] as GameObject);
				rowParents [row] = null;
				if (row == 0) {
					int h = 0;
					rowParents.RemoveAt (0);
					while (h < alienRows.Count && checkRowEmpty (h)) {
						alienRows.RemoveAt (0);
					}
					if (alienRows.Count == 0) {
						return;
					}
					int i = 0;
					int firstRow = 1;
					if (rowParents.Count > 1) {
						while (i < rowParents.Count && rowParents [i] == null) {	// find first real row of aliens
							i++;
						}
						firstRow = i + 1;
					}
					for (int j = 0; j < alienRows.Count; j++) {
						for (int k = 0; k < (alienRows [j] as ArrayList).Count; k++) {
							if ((alienRows [j] as ArrayList) [k] != null) {
								AlienScript theAlien = ((alienRows [j] as ArrayList) [k] as GameObject).GetComponent<AlienScript> ();
								theAlien.setArrayPosition (theAlien.getRow () - firstRow, theAlien.getCol ());
							}
						}
					}
					int l = 0;
					while (l < rowParents.Count && rowParents [l] == null) {
						rowParents.RemoveAt (l);
					}
				} else if (row == alienRows.Count - 1) {
					rowParents.RemoveAt (alienRows.Count - 1);
					while (alienRows.Count > 0 && checkRowEmpty (alienRows.Count - 1)) {
						alienRows.RemoveAt (alienRows.Count - 1);
					}
					if (alienRows.Count == 0) {
						return;
					}
					while (rowParents.Count > 0 && rowParents [rowParents.Count - 1] == null) {
						rowParents.RemoveAt (rowParents.Count - 1);
					}
				}
			}
		}
	}
}
