using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

	private BoxCollider2D m_BackgroundCollider;
	private float m_BackgroundSize;

	// Use this for initialization
	void Start () {
		m_BackgroundCollider = GetComponent<BoxCollider2D> ();
		m_BackgroundSize = m_BackgroundCollider.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -m_BackgroundSize)
			RepeatBackground ();	
	}

	void RepeatBackground () {
		Vector2 BGoffset = new Vector2 (m_BackgroundSize * 5f, 0);
		transform.position = (Vector2)transform.position + BGoffset;
	}
}
