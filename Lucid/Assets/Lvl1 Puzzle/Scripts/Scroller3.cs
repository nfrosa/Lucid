using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scroller3 : MonoBehaviour {

	private Rigidbody2D RIGIDBODY;
	private float m_Speed = -0.5f;
	[SerializeField] private bool m_StopScrolling;
	int count = 0;

	// Use this for initialization
	void Start () {
		RIGIDBODY = GetComponent<Rigidbody2D> ();
		RIGIDBODY.velocity = new Vector3 (m_Speed, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (m_StopScrolling) {
			RIGIDBODY.velocity = Vector3.zero;
		}
		else{
			RIGIDBODY.velocity = new Vector3 (m_Speed, 0, 0);
			count++;
		}

		if(count == 12200)
			SceneManager.LoadScene("Menu");
	}
}
