using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

	public SpriteRenderer sr;
	public string power;

	// Use this for initialization
	void Start ()
	{
		setPower (power);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void setPower (string power)
	{
		this.power = power;
		sr.sprite = Resources.Load<Sprite> ("Art/" + power + " powerup");
	}

	public string getPower ()
	{
		return power;
	}
}
