using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
	public GameObject attackPrefab;
	public Material whiteMaterial;

	Music music;
	Transform attack;
	new CircleCollider2D collider;
	new SpriteRenderer renderer;
	float speed = 2;
	int number;
	
	void Start () {
		attack = transform.GetChild(0);
		number = Array.IndexOf(FindObjectsOfType<PlayerControl>(), this) + 1;
		music = FindObjectOfType<Music>();
		collider = GetComponent<CircleCollider2D>();
		renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		// -- Movement --
		Vector2 delta = new Vector2(Input.GetAxis("Horizontal"+number), Input.GetAxis("Vertical"+number)) * speed;

		attack.position = (Vector2)transform.position + delta;
		
		if (music.beat) {
			// -- Attacks --
			if (Input.GetButton("Attack"+number)) {
				Projectile projectile = ((GameObject)Instantiate(attackPrefab, transform.position, Quaternion.identity)).GetComponent<Projectile>();
				projectile.initialize(delta.normalized * 4, number, renderer.color);
			}

			transform.position += (Vector3)delta;

			// -- Collision --
			int otherNumber = number == 1 ? 2 : 1;
			Collider2D other = Physics2D.OverlapCircle(transform.position, collider.radius, LayerMask.GetMask("P"+otherNumber+"Projectile"));
			if (other != null) {
				Debug.Log(name+" hit "+other.name);

				SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
				SpriteRenderer otherRenderer = other.GetComponent<SpriteRenderer>();
				thisRenderer.material = whiteMaterial;
				otherRenderer.material = whiteMaterial;
				thisRenderer.color = Color.white;
				otherRenderer.color = Color.white;

				int deathCam = LayerMask.NameToLayer("DeathCam");

				gameObject.layer = deathCam;
				other.gameObject.layer = deathCam;

				Camera.main.backgroundColor = Color.black;
				Camera.main.cullingMask = LayerMask.GetMask("DeathCam");

				Time.timeScale = 0; //pause game
				StartCoroutine(twoSecondReset()); //wait 2 seconds and reset
			}
		}
	}

	IEnumerator twoSecondReset() {
		yield return new WaitForSecondsRealtime(2);

		Time.timeScale = 1; //unpause game
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload
	}
}
