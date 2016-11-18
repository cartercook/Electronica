using UnityEngine;
using System;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour {
	Transform attack;
	float speed = 2;
	int number;
	float counter = 0;

	// Use this for initialization
	void Start () {
		attack = transform.GetChild(0);
		number = Array.IndexOf(FindObjectsOfType<PlayerControl>(), this) + 1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 delta = new Vector2(Input.GetAxis("Horizontal"+number), Input.GetAxis("Vertical"+number)) * speed;

		attack.position = (Vector2)transform.position + delta;

		counter += Time.deltaTime;
		if (counter > 1) {
			transform.Translate(delta);
			counter -= 1;
		}
	}
}
