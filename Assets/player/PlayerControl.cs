using UnityEngine;
using System;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour {
	public GameObject attackPrefab;

	Music music;
	Transform attack;
	float speed = 2;
	int number;
	
	void Start () {
		attack = transform.GetChild(0);
		number = Array.IndexOf(FindObjectsOfType<PlayerControl>(), this) + 1;
		music = FindObjectOfType<Music>();
	}
	
	void Update () {
		// -- Movement --
		Vector2 delta = new Vector2(Input.GetAxis("Horizontal"+number), Input.GetAxis("Vertical"+number)) * speed;

		attack.position = (Vector2)transform.position + delta;
		
		if (music.beat) {
			// -- Attacks --
			if (Input.GetButton("Attack"+number)) {
				Projectile projectile = ((GameObject)Instantiate(attackPrefab, transform.position, Quaternion.identity)).GetComponent<Projectile>();
				projectile.initialize(number, delta.normalized * 4);
			}

			transform.position += (Vector3)delta;
		}
	}
}
