using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour {

	public GameObject alienBullet;

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
}
