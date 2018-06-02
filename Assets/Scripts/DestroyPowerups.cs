using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerups : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		foreach (GameObject power in GameObject.FindGameObjectsWithTag("powerup")) {
			Destroy (power);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
