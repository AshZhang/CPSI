﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

	public float yVel;
	public Rigidbody2D rb;
	public GameObject explosion;

	// Use this for initialization
	public virtual void Start ()
	{
		rb.velocity = new Vector3 (0, yVel, 0);
	}

	// Update is called once per frame
	public virtual void Update ()
	{
		if (transform.position.y > 6) {
			Destroy (this.gameObject);
		}
	}

	public virtual void OnDestroy ()
	{
		GameObject.Find ("Player").GetComponent<PlayerControl> ().deleteBullet ();
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Bullet") {
			Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
		}
		if (coll.gameObject.name == "Alien 1(Clone)") {
			Destroy (this.gameObject);
			AlienMovement alienArray = GameObject.Find ("AlienParent").GetComponent<AlienMovement> ();
			AlienScript deadAlien = coll.gameObject.GetComponent<AlienScript> ();
			alienArray.removeAlien (deadAlien.getRow (), deadAlien.getCol ());
		}
	}

}