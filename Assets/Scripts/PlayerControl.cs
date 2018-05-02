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
	public GameObject explosion;

	private int lives;
	private int maxBullets;
	private int numBullets;
	private int numSpecialShots;
	private string powerupMode;
	// 0 = normal, 1 = loop, 2 = arrays, 3 = OOP, 4 = recursion

	// Use this for initialization
	void Start ()
	{
		lives = 3;
		livesText.text = "Lives: " + lives;
		maxBullets = 1;
		numBullets = 0;
		numSpecialShots = 0;
		powerupMode = "none";

	}
	
	// Update is called once per frame
	void Update ()
	{
		rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * xVel, 0);
		if (Input.GetButton ("Fire1") && numBullets < maxBullets) {
			if (numSpecialShots == 0) {
				powerupMode = "none";
				maxBullets = 1;
			} else {
				numSpecialShots--;
			}
			switch (powerupMode) {
			case "loop":
				for (int i = 0; i < maxBullets; i++) {
					Instantiate (bullet, new Vector3 (transform.position.x + (i - 1) * 0.5f, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
					numBullets++;
				}
				break;
			case "arraylist":
				Instantiate (arrayBullet, new Vector3 (transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
				numBullets++;
				break;
			case "OOP":
				break;
			case "TA":
				break;
			default:
				Instantiate (bullet, new Vector3 (transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
				numBullets++;
				break;
			}
		}
		if (lives <= 0) {
			Destroy (gameObject);
			SceneManager.LoadScene ("Lose");
		}

	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name == "AlienBullet(Clone)" || coll.gameObject.name == "Alien 1(Clone)") {
			lives--;
			Instantiate (explosion, transform.position, Quaternion.identity);
			livesText.text = "Lives: " + lives;
			Destroy (coll.gameObject);
		} else if (!coll.gameObject.name.Contains ("Side Walls") && !coll.gameObject.name.Contains ("Alien 1")) {
			string objName = coll.gameObject.GetComponent<Powerup> ().getPower ();
			switch (objName) {
			case "loop":
				maxBullets = 3;
				numSpecialShots = 5;
				break;
			case "arraylist":
				maxBullets = 1;
				numSpecialShots = 3;
				break;
			case "OOP":
				break;
			case "TA":
				break;
			case "pumpkin":
				lives++;
				break;
			default:
				break;
			}
			if (objName != "pumpkin") {
				powerupMode = objName;
			}
			Destroy (coll.gameObject);
		}
	}

	public void deleteBullet ()
	{
		numBullets--;
	}
}
