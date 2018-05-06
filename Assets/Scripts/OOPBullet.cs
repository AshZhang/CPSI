using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOPBullet : BulletMovement {

	public GameObject bullet;

	public override void Update(){
		base.Update ();
		if (Input.GetButton ("Fire1") && transform.position.y > -2) {
			createBullets ();
		}
	}

	public void createBullets(){
		for (int i = 0; i < 3; i++) {
			float angle = 30 * (1-i);
			GameObject newBullet = Instantiate (bullet, new Vector3 (transform.position.x + 0.2f * (i-1), transform.position.y + 0.25f, transform.position.z), Quaternion.Euler(0, 0, angle));
			BulletMovement theBullet = newBullet.GetComponent<BulletMovement> ();
			theBullet.setVelocity(new Vector3(-theBullet.getVelocity() * Mathf.Sin(angle * Mathf.Deg2Rad), theBullet.getVelocity() * Mathf.Cos(angle * Mathf.Deg2Rad), 0));
			GameObject.Find ("Player").GetComponent<PlayerControl> ().addBullet ();

		}
		Destroy (this.gameObject);
	}

	public float getY(){
		return transform.position.y;
	}

}
