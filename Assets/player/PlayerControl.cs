using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
	public GameObject attackPrefab; //moving wall
	public Material whiteMaterial;

	int number;
	PlayerShadow shadow;
	Transform attack;
	new CircleCollider2D collider;
	new SpriteRenderer renderer;
	
	void Start () {
		number = Array.IndexOf(FindObjectsOfType<PlayerControl>(), this) + 1;

		shadow = transform.GetChild(0).GetComponent<PlayerShadow>();
		shadow.initialize(number);

		attack = transform.GetChild(1);

		collider = GetComponent<CircleCollider2D>();
		renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		// -- Move Shadow --
		Vector2 delta = new Vector2(Input.GetAxis("Controller"+number+"Stick1X"), Input.GetAxis("Controller"+number+"Stick1Y")) * collider.radius * 2;

		shadow.transform.position = (Vector2)transform.position + delta;
		
		if (Music.beat) {
			// -- Wall Attack --
			if (Input.GetButton("Attack"+number)) {
				Projectile projectile = ((GameObject)Instantiate(attackPrefab, transform.position, Quaternion.identity)).GetComponent<Projectile>();
				projectile.initialize(delta.normalized * 4, number, renderer.color);
			}

			// -- Circle Attack --
			transform.position += (Vector3)delta;
			attack.localPosition = shadow.attackPosDelta;

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

				//only render these two
				gameObject.layer = LayerMask.NameToLayer("DeathCam"); ;
				other.gameObject.layer = LayerMask.NameToLayer("DeathCam");

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
