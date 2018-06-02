using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string gameMode = GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel ();
		GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/" + gameMode + "/spaceship");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
