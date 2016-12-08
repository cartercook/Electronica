using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	public Vector2 attackPosDelta;

	Transform attack;
	float attackRadius;
	int number;

	// Use this for initialization
	public void initialize(int number) {
		attack = transform.GetChild(0);
		attackRadius = attack.GetComponent<SpriteRenderer>().bounds.extents.x;
		this.number = number;
	}
	
	// Update is called once per frame
	void Update () {
		// -- Movement --
		attackPosDelta = new Vector2(Input.GetAxis("Controller"+number+"Stick2X"), Input.GetAxis("Controller"+number+"Stick2Y")) * attackRadius;

		attack.position = (Vector2)transform.position + attackPosDelta;
	}


}
