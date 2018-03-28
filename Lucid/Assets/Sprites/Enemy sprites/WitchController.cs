using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "lucid_dreamSprites_0") {
			Destroy (other.gameObject);
		}
	}
}
