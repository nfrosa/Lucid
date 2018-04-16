using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller2 : MonoBehaviour {

	private Rigidbody2D RIGIDBODY;
	private float m_Speed = -0.3f;
	[SerializeField] private bool m_StopScrolling;

	// Use this for initialization
	void Start () {
		RIGIDBODY = GetComponent<Rigidbody2D> ();
		RIGIDBODY.velocity = new Vector2 (m_Speed, 0);
	}

	// Update is called once per frame
	void Update () {
		if (m_StopScrolling)
			RIGIDBODY.velocity = Vector2.zero;
		else
			RIGIDBODY.velocity = new Vector2 (m_Speed, 0);
	}
}
