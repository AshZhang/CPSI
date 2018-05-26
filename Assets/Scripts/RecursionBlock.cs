using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		switch (coll.gameObject.tag) {
		case "Bullet":
		case "powerup":
			Destroy (coll.gameObject);
			break;
		case "AlienBullet":
			Destroy (coll.gameObject);
			Destroy (gameObject);
			break;
		default:
			break;
		}
	}

}
