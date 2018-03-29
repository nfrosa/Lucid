using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Animator animator;
    
	// Define player move speed
	public float moveSpeed;

	// Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        animator.SetBool("Idle", false);
        if (vertical < 0)
        {
            animator.SetInteger("Direction", 0); // going down
			transform.Translate (new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }
        else if (horizontal > 0)
        {
            animator.SetInteger("Direction", 1); // going right
			transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }
        else if (vertical > 0)
        {
            animator.SetInteger("Direction", 2); // going up
			transform.Translate (new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("Direction", 3); // going left
			transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }
        else
        {
            animator.SetBool("Idle", true);
        }

    }
    // src: http://michaelcummings.net/mathoms/creating-2d-animated-sprites-using-unity-4.3
}
