using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrangeGameDisplay : MonoBehaviour {

	public Text endGameText;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel() == "War Games") {
			endGameText.alignment = TextAnchor.UpperLeft;
			endGameText.text = "Greetings Ms. Paymer\nHello\nA strange game.\nThe only winning move is to spam the spacebar.\nHow about another game of Ms. Paymer's Space Invaders?";
			Destroy (GameObject.Find ("Lose Title"));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
