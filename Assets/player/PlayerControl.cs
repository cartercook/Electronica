using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
	public GameObject attackPrefab; //moving wall
	public Material SolidColour;

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
		Vector2 leftStickDelta = Vector2.ClampMagnitude(
			new Vector2(Input.GetAxis("Controller"+number+"Stick1X"), Input.GetAxis("Controller"+number+"Stick1Y"))*collider.radius*2,
			collider.radius*2
		);
		shadow.transform.position = (Vector2)transform.position + leftStickDelta;

		Vector2 rightStickDelta = shadow.Update();

		if (Music.beat) {
			// -- Wall Attack --
			if (Input.GetButton("Attack"+number)) {
				Projectile projectile = ((GameObject)Instantiate(attackPrefab, transform.position, Quaternion.identity)).GetComponent<Projectile>();
				projectile.initialize(rightStickDelta.normalized * 4, number, renderer.color);
			}

			// -- Circle Attack --
			transform.position += (Vector3)leftStickDelta;
			attack.localPosition = rightStickDelta;
		}
	}

	void LateUpdate() {
		// -- Collision --
		if (Music.beat) {
			int otherNumber = number == 1 ? 2 : 1;
			Collider2D other = Physics2D.OverlapCircle(transform.position, collider.radius, LayerMask.GetMask("P"+otherNumber+"Projectile"));
			if (other != null) {
				Debug.Log(name+" hit "+other.name);

				//colour white
				SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
				SpriteRenderer otherRenderer = other.GetComponent<SpriteRenderer>();
				thisRenderer.material = SolidColour;
				otherRenderer.material = SolidColour;

				//only render these two
				gameObject.layer = LayerMask.NameToLayer("DeathCam");
				other.gameObject.layer = LayerMask.NameToLayer("DeathCam");
				Camera.main.cullingMask = LayerMask.GetMask("DeathCam");

				//black background
				Camera.main.backgroundColor = Color.black;

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
