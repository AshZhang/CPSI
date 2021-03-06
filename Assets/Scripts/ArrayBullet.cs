﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBullet : BulletMovement
{

	public override void Start ()
	{
		base.Start ();
		GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/arraylist bullet");
		GetComponent<BoxCollider2D> ().size = GetComponent<SpriteRenderer> ().sprite.bounds.size;
	}

	public override void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Bullet") {
			Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
		}
		if (coll.gameObject.name == "Alien 1(Clone)") {
			
			Destroy (this.gameObject);
			AlienMovement alienArray = GameObject.Find ("AlienParent").GetComponent<AlienMovement> ();
			AlienScript deadAlien = coll.gameObject.GetComponent<AlienScript> ();
			alienArray.removeAlien (deadAlien.getRow () - 1, deadAlien.getCol (), "arraylist");
			alienArray.removeAlien (deadAlien.getRow () + 1, deadAlien.getCol (), "arraylist");
			alienArray.removeAlien (deadAlien.getRow (), deadAlien.getCol () - 1, "arraylist");
			alienArray.removeAlien (deadAlien.getRow (), deadAlien.getCol (), "arraylist");

		}
	}
}
