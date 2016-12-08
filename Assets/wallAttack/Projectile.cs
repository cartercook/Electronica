using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour {
	Music music;
	Shadow shadow;

	void Update() {
		if (music.beat) {
			transform.position = shadow.transform.position;
			shadow.transform.localPosition = Vector2.zero;
		}
	}

	public void initialize(Vector2 direction, int layer, Color color) {
		music = FindObjectOfType<Music>();
		shadow = transform.GetChild(0).GetComponent<Shadow>();

		name = "P"+layer+" "+name;

		gameObject.layer = LayerMask.NameToLayer("P"+layer+"Projectile");

		GetComponent<SpriteRenderer>().color = color;

		transform.right = -direction; //rotate tranform.right to be parallel to direction
		shadow.velocity = direction;
	}
}