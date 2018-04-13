using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {
	public int playerMaxHealth;
	public int playerCurrentHealth;

	private bool flashActive;
	public float flashLength;
	private float flashCounter;

	private SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
		playerCurrentHealth = playerMaxHealth;
		playerSprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCurrentHealth <= 0) {
			gameObject.SetActive(false);
		}

		if (flashActive) {

			if (flashCounter > flashLength * .66f) {
				playerSprite.color = new Color (playerSprite.color.r, 0f, 0f, playerSprite.color.a);
			} else if (flashCounter > flashLength * .33f) {
				playerSprite.color = new Color (playerSprite.color.r, 1f, 1f, playerSprite.color.a);
			} else if (flashCounter > 0) {
				playerSprite.color = new Color (playerSprite.color.r, 0f, 0f, playerSprite.color.a);
			} else {
				playerSprite.color = new Color (playerSprite.color.r, 1f, 1f, playerSprite.color.a);
				flashActive = false;
			}

			flashCounter -= Time.deltaTime;
		}
	}

	public void HurtPlayer(int damageToGive) {
		playerCurrentHealth -= damageToGive;
		flashActive = true;
		flashCounter = flashLength;
	}

	public void setMaxHealth() {
		playerCurrentHealth = playerMaxHealth;
	}
}
