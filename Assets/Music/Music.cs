using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public const float BPS = 1;
	
	public static bool beat {
		get { return counter > BPS; }
	}

	static float counter = 0;
	new AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource>();
	}

	//NOTE: executes before all other Update functions since
	//Music is set in Project Settings -> Script Execution Order 
	void Update () {
		if (counter > BPS) {
			counter -= BPS;
		}

		counter += Time.deltaTime;

		if (counter > 1) {
			audio.Play();
		}
	}
}
