using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour {

	public GameObject alienBullet;
	public GameObject powerup;

	private int row;
	private int col;

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider2D> ().size = GetComponent<SpriteRenderer> ().sprite.bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
		int rand = (int)Random.Range (1, 1001);
		if (rand == 1) {
			Instantiate (alienBullet, transform.position, Quaternion.identity);
			GetComponent<AudioSource> ().Play ();
		}
	}

	public void setArrayPosition(int row, int col){
		this.row = row;
		this.col = col;
	}

	public void setSprite(Sprite sp){
		GetComponent<SpriteRenderer> ().sprite = sp;
	}

	public int getCol(){
		return col;
	}

	public int getRow(){
		return row;
	}

	public Vector3 getPos(){
		return transform.position;
	}

	void OnDestroy(){
		if (Random.Range (1, 7) == 1) {
			int power = Random.Range (1, 7);
			Powerup newPower = Instantiate (powerup, transform.position, Quaternion.identity).GetComponent<Powerup>();
			switch (power) {
			case 1:
				newPower.setPower ("loop");
				break;
			case 2:
				newPower.setPower ("arraylist");
				break;
			case 3:
				newPower.setPower ("OOP");
				break;
			case 4:
				newPower.setPower ("TA");
				break;
			case 5:
				newPower.setPower ("pumpkin");
				break;
			case 6:
				newPower.setPower ("recursion");
				break;
			default: break;
			}
		}
	}
}
