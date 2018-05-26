using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{

	public Animator anim;

	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 5);
		if (GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel () == "War Games") {
			setType ("war games");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("End")) {
			Destroy (gameObject);
		}
	}

	public void setType (string type)
	{
		if (type != "") {
			type += " ";
		}
		anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animations/" + type + "explosion controller") as RuntimeAnimatorController;
		anim.gameObject.SetActive (true);
		anim.Play (type + "explosion");
	}
}
