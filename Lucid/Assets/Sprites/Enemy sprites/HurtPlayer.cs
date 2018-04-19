using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour {

	public int damageToGive;
	public int curHealth;
	public int maxHealth = 30;

	private bool flashActive;
	public float flashLength;
	private float flashCounter;

	private Animator animator;

	private SpriteRenderer enemySprite;

	public AudioSource death;
	public AudioClip death1;

	void Start()
	{
		curHealth = maxHealth;
		enemySprite = GetComponent<SpriteRenderer> ();
		animator = this.GetComponent<Animator>();
	}

	void Update()
	{
		if (curHealth <= 0) {
			animator.SetBool ("isDying", true);
			death.PlayOneShot (death1);
			StartCoroutine (Coroutine ());
		}
		if (flashActive) {

			if (flashCounter > flashLength * .66f) {
				enemySprite.color = new Color (enemySprite.color.r, 0f, 0f, enemySprite.color.a);
			} else if (flashCounter > flashLength * .33f) {
				enemySprite.color = new Color (enemySprite.color.r, 1f, 1f, enemySprite.color.a);
			} else if (flashCounter > 0) {
				enemySprite.color = new Color (enemySprite.color.r, 0f, 0f, enemySprite.color.a);
			} else {
				enemySprite.color = new Color (enemySprite.color.r, 1f, 1f, enemySprite.color.a);
				flashActive = false;
			}

			flashCounter -= Time.deltaTime;
		}
	}

	IEnumerator Coroutine() {
		yield return new WaitForSeconds(0.4f); //this will wait 5 seconds
		if (gameObject.name == "Boss") {
			SceneManager.LoadScene("Puzzle");
		} else {
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
		flashActive = true;
		flashCounter = flashLength;
	}
}
