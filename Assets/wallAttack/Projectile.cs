using UnityEngine;

public class Projectile : MonoBehaviour {
	WallShadow shadow;

	void Update() {
		if (Music.beat) {
			transform.position = shadow.transform.position;
			shadow.transform.localPosition = Vector2.zero;
		}
	}

	public void initialize(Vector2 direction, int layer, Color colour) {
		shadow = transform.GetChild(0).GetComponent<WallShadow>();

		name = "P"+layer+" "+name;

		gameObject.layer = LayerMask.NameToLayer("P"+layer+"Projectile");

		GetComponent<SpriteRenderer>().color = colour;
		SpriteRenderer shadowRenderer = shadow.GetComponent<SpriteRenderer>();
		colour.a = shadowRenderer.color.a;
		shadowRenderer.color = colour;

		transform.right = -direction; //rotate tranform.right to be parallel to direction
		shadow.velocity = direction;
	}
}