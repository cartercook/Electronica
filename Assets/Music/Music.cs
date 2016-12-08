using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	
	public static bool beat {
		get { return counter > 1; }
	}

	static float counter = 0;
	new AudioSource audio;

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
