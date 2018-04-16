using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 rbvel;
    private Vector2 oldpos;


    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 vel = rb.position - oldpos;
        oldpos = rb.position;
        rbvel = vel;
        animator.SetBool("Idle", false);
        if (vel.x == 0 && vel.y <0)
        {
            animator.SetInteger("Direction", 0); // going down
        }
        else if (vel.x > 0 && vel.y == 0)
        {
            animator.SetInteger("Direction", 1); // going right
        }
        else if (vel.x == 0 && vel.y > 0)
        {
            animator.SetInteger("Direction", 2); // going up
        }
        else if (vel.x < 0 && vel.y == 0)
        {
            animator.SetInteger("Direction", 3); // going left
        }
        else
        {
            animator.SetBool("Idle", true);
        }

    }
}
