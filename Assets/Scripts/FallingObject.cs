using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{

	public float yVel;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
		rb.velocity = new Vector3 (0, yVel, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < -6) {
			Destroy (this.gameObject);
		}
	}

}
