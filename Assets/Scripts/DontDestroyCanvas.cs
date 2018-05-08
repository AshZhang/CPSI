using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvas : MonoBehaviour {

	private static DontDestroyCanvas instance = null;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			return;
		}
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
