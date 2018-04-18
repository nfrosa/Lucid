using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;

	private float attackTimer = 0;
	private float attackCd = 0.3f;

	public Collider2D attackTrigger;
	public AudioSource punch;
	public AudioClip punch1;

	private Animator anim;

	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown ("f") && !attacking) {
			attacking = true;
			attackTimer = attackCd;
		
			attackTrigger.enabled = true;
			punch.PlayOneShot (punch1);
		}

		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}
		anim.SetBool ("Attacking", attacking);
	}
}
