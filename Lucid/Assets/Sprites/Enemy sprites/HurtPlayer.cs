using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int damageToGive;
	public int curHealth;
	public int maxHealth = 30;

	void Start()
	{
		curHealth = maxHealth;
	}

	void Update()
	{
		if (curHealth <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealthManager> ().HurtPlayer(damageToGive);
		}
	}

	public void Damage(int damage){
		curHealth -= damage;

	}
}
