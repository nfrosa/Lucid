using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour {
	public float waitToReload;
	private bool reloading; 
	private GameObject thePlayer;

	void Update() {
		if (reloading) {
			waitToReload -= Time.deltaTime;
			if (waitToReload < 0) {
				Application.LoadLevel(Application.loadedLevel);
				thePlayer.SetActive(true);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "lucid_dreamSprites_0") {
			other.gameObject.SetActive(false);
			reloading = true;
			thePlayer = other.gameObject;
		}
	}
}
