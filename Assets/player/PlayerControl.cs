using UnityEngine;
using XboxCtrlrInput;
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
			new Vector2(
				InputManager.getAxis(XboxAxis.LeftStickX, (XboxController)number),
				InputManager.getAxis(XboxAxis.LeftStickY, (XboxController)number)
			)*collider.radius*2,
			collider.radius*2
		);
		shadow.transform.position = (Vector2)transform.position + leftStickDelta;

		Vector2 rightStickDelta = shadow.Update();

		if (Music.beat) {
			// -- Wall Attack --
			Boolean attackPressed = false;
			switch (number) {
				case 1:
					attackPressed = Input.GetKey(KeyCode.Space);
					break;
				case 2:
					attackPressed = Input.GetKey(KeyCode.Keypad0);
					break;
			}
			attackPressed = attackPrefab || XCI.GetButton(XboxButton.RightBumper, (XboxController)number);

			if (attackPressed && rightStickDelta.magnitude > 0.5) {
				Projectile projectile = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
				projectile.initialize(rightStickDelta.normalized * 4, number, renderer.color);
			}

			// -- Circle Attack --
			transform.position += (Vector3)leftStickDelta;
			attack.localPosition = rightStickDelta;

			// -- Lazer Attack --
			if (rightStickDelta.magnitude > 0.5 && XCI.GetButton(XboxButton.LeftBumper, (XboxController)number)) {
				
			}
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
