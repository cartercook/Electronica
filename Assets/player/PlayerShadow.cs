using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	Transform attackShadow;
	float attackRadius;
	int number;

	public void initialize(int number) {
		attackShadow = transform.GetChild(0);
		attackRadius = attackShadow.GetComponent<SpriteRenderer>().bounds.size.x;
		this.number = number;
	}
	
	//called by parent
	public Vector2 Update() {
		Vector2 rightStickDelta = Vector2.ClampMagnitude(
			new Vector2(Input.GetAxis("Controller"+number+"Stick2X"), Input.GetAxis("Controller"+number+"Stick2Y"))*attackRadius,
			attackRadius
		);

		attackShadow.position = (Vector2)transform.position + rightStickDelta;

		return rightStickDelta;
	}


}
