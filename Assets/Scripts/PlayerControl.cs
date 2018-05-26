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
	public GameObject OOPBullet;
	public GameObject TAship;
	public GameObject recurBlock;
	public Text livesText;
	public GameObject explosion;
	public GameObject jerooBG;
	public AudioClip laserShoot;
	public AudioClip itemGet;

	private int lives;
	private int maxBullets;
	private int numBullets;
	private int numSpecialShots;
	private string powerupMode;
	private string gameMode;
	private int numTAships;

	// Use this for initialization
	void Start ()
	{
		lives = 3;
		livesText.text = "Lives: " + lives;
		maxBullets = 1;
		numBullets = 0;
		numSpecialShots = 0;
		numTAships = 0;
		powerupMode = "none";
		gameMode = GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel ();
		GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/" + gameMode + "/spaceship");
		GetComponent<BoxCollider2D> ().size = GetComponent<SpriteRenderer> ().sprite.bounds.size;
		if (gameMode == "Jeroo") {
			Instantiate (jerooBG, new Vector3(0, 0, 5), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * xVel, 0);
		if (Input.GetButton ("Fire1") && numBullets < maxBullets) {
			GetComponent<AudioSource> ().clip = laserShoot;
			GetComponent<AudioSource> ().Play ();
			if (numSpecialShots == 0) {
				powerupMode = "none";
				maxBullets = 1;
			} else {
				numSpecialShots--;
			}
			switch (powerupMode) {
			case "loop":
				for (int i = 0; i < maxBullets; i++) {
					Instantiate (bullet, new Vector3 (transform.position.x + (i - 1) * 0.5f, transform.position.y + 0.35f + 0.5f * GetComponent<BoxCollider2D>().size.y, transform.position.z), Quaternion.identity);
					numBullets++;
				}
				break;
			case "arraylist":
				Instantiate (arrayBullet, new Vector3 (transform.position.x, transform.position.y + 0.35f + 0.5f * GetComponent<BoxCollider2D>().size.y, transform.position.z), Quaternion.identity);
				numBullets++;
				break;
			case "OOP":
				Instantiate (OOPBullet, new Vector3 (transform.position.x, transform.position.y + 0.35f + 0.5f * GetComponent<BoxCollider2D> ().size.y, transform.position.z), Quaternion.identity);
				numBullets++;
				break;
			default:
				Instantiate (bullet, new Vector3 (transform.position.x, transform.position.y + 0.35f + 0.5f * GetComponent<BoxCollider2D>().size.y, transform.position.z), Quaternion.identity);
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
		} else if (coll.gameObject.tag == "powerup") {
			string objName = coll.gameObject.GetComponent<Powerup> ().getPower ();
			GetComponent<AudioSource> ().clip = itemGet;
			GetComponent<AudioSource> ().Play ();
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
				maxBullets = 1;
				numSpecialShots = 3;
				break;
			case "TA":
				while (numTAships < 2) {
					Instantiate (TAship, new Vector3 (Random.Range (-3f, 3f), transform.position.y, transform.position.z), Quaternion.identity);
					numTAships++;				
				}
				break;
			case "recursion":
				GameObject[] curBlocks = GameObject.FindGameObjectsWithTag ("recursion block");
				foreach(GameObject block in curBlocks){
					Destroy (block);
				}
				for (int r = 0; r < 3; r++) {
					for (int c = 0; c < 3; c++) {
						Instantiate (recurBlock, new Vector3(-4 + r * 4, -2 + 0.4f * c, 0), Quaternion.identity);
					}
				}
				break;
			case "pumpkin":
				lives++;
				livesText.text = "Lives: " + lives;
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

	public void addBullet ()
	{
		numBullets++;
	}
	public void deleteTAship(){
		numTAships--;
	}
}
