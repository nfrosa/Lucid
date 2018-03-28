using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Animator animator;
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
        }
        else if (horizontal > 0)
        {
            animator.SetInteger("Direction", 1); // going right
        }
        else if (vertical > 0)
        {
            animator.SetInteger("Direction", 2); // going up
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("Direction", 3); // going left
        }
        else
        {
            animator.SetBool("Idle", true);
        }

    }
    // src: http://michaelcummings.net/mathoms/creating-2d-animated-sprites-using-unity-4.3
}
