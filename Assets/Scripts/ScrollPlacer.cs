using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollPlacer : MonoBehaviour {

	private static ScrollPlacer instance = null;

	public Canvas theCanvas;

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
		if (SceneManager.GetActiveScene ().name == "Start") {
			theCanvas.gameObject.SetActive (true);
		}else{
			theCanvas.gameObject.SetActive (false);
		}
	}
}
