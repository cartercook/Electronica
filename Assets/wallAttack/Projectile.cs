using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour {
	public Material whiteMaterial;

	Music music;
	Shadow shadow;

	void Update() {
		if (music.beat) {
			transform.position = shadow.transform.position;
			shadow.transform.localPosition = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
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

	IEnumerator twoSecondReset() {
		yield return new WaitForSecondsRealtime(2);

		Time.timeScale = 1; //unpause game
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload
	}

	public void initialize(int layer, Vector2 direction) {
		music = FindObjectOfType<Music>();
		shadow = transform.GetChild(0).GetComponent<Shadow>();

		name = "P"+layer+" "+name;

		gameObject.layer = LayerMask.NameToLayer("P"+layer+"Projectile");

		transform.right = -direction; //rotate tranform.right to be parallel to direction
		shadow.velocity = direction;
	}
}