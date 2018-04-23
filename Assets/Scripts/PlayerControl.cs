using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

	public Rigidbody2D rb;
	public float xVel;
	public GameObject bullet;
	public GameObject arrayBullet;
	public Text livesText;

	private int lives;
	private int maxBullets;
	private int numBullets;
	private int numSpecialShots;
	private int powerupMode;
	// 0 = normal, 1 = loop, 2 = arrays, 3 = OOP, 4 = recursion

	// Use this for initialization
	void Start ()
	{
		lives = 3;
		livesText.text = "Lives: " + lives;
		maxBullets = 1;
		numBullets = 0;
		numSpecialShots = 0;
		powerupMode = 0;

	}
	
	// Update is called once per frame
	void Update ()
	{
		rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * xVel, 0);
		if (Input.GetButton ("Fire1") && numBullets < maxBullets) {
			if (numSpecialShots == 0) {
				powerupMode = 0;
				maxBullets = 1;
			} else {
				numSpecialShots--;
			}
			switch (powerupMode) {
			case 1:
				for (int i = 0; i < maxBullets; i++) {
					Instantiate (bullet, new Vector3 (transform.position.x + (i-1) * 0.5f, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
					numBullets++;
				}
				break;
			case 2:
				Instantiate (arrayBullet, new Vector3 (transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
				break;
			case 3:
				break;
			case 4:
				break;
			default:
				Instantiate (bullet, new Vector3 (transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
				numBullets++;
				break;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name == "AlienBullet(Clone)" || coll.gameObject.name == "Alien 1(Clone)") {
			lives--;
			if (lives < 0) {
				SceneManager.LoadScene ("Lose");
			}
			livesText.text = "Lives: " + lives;
		} else if (coll.gameObject.name == "Loop Powerup") {
			maxBullets = 3;
			numSpecialShots = 5;
			powerupMode = 1;
		} else if (coll.gameObject.name == "Array Powerup") {
			maxBullets = 1;
			numSpecialShots = 3;
			powerupMode = 2;
		}
		if (!coll.gameObject.name.Contains("Side Walls") && !coll.gameObject.name.Contains("Alien 1")) {
			Destroy (coll.gameObject);
		}
	}

	public void deleteBullet ()
	{
		numBullets--;
	}
}
