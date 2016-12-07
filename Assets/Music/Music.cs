using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	float counter = 0;
	new AudioSource audio;

	public bool beat {
		get { return counter > 1; }
	}

	void Start() {
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		if (counter > 1) {
			counter -= 1;
		}

		counter += Time.deltaTime;

		if (counter > 1) {
			audio.Play();
		}
	}
}
