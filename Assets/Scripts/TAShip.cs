﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAShip : MonoBehaviour
{

	public GameObject bullet;
	public GameObject explosion;
	public Rigidbody2D rb;
	public float xVel;

	private GameObject curBullet;
	private int lives;

	// Use this for initialization
	void Start ()
	{
		rb.velocity = new Vector3 (xVel, 0, 0);
		lives = 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x < -7.75 || transform.position.x > 7.75) {
			xVel *= -1;
			transform.position = new Vector3 (-7.74f * (xVel / Mathf.Abs (xVel)), transform.position.y, transform.position.z);
		}
		rb.velocity = new Vector3 (xVel, 0, 0);
		if (curBullet == null) {
			curBullet = Instantiate (bullet, new Vector3 (transform.position.x, transform.position.y + 0.35f + 0.5f * GetComponent<BoxCollider2D>().size.y, transform.position.z), Quaternion.identity);
			curBullet.GetComponent<BulletMovement> ().shouldDelete = false;
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "AlienBullet" || coll.gameObject.name == "Alien 1(Clone") {
			Instantiate (explosion, transform.position, Quaternion.identity);
			lives--;
			if (lives <= 0) {
				Destroy (this.gameObject);
			}
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "powerup") {
			Destroy (coll.gameObject);
		} else {
			Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
		}
	}
}