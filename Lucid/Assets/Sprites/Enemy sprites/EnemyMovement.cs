using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private SpriteRenderer enemySprite;
	private Rigidbody2D rigidbody2D;
	private Vector2 rbvel;
	private Vector2 oldpos;

	// Use this for initialization
	void Start () {
		enemySprite = GetComponent<SpriteRenderer> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 vel = rigidbody2D.position - oldpos;
		oldpos = rigidbody2D.position;
		rbvel = vel;
		if (vel.x > 0) {
			enemySprite.flipX = false;
		} else if (vel.x < 0) {
			enemySprite.flipX = true;
		} 
	}
}
