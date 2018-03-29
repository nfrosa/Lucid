using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int damageToGive;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "lucid_dreamSprites_0") {
			other.gameObject.GetComponent<PlayerHealthManager> ().HurtPlayer(damageToGive);
		}
	}
}
