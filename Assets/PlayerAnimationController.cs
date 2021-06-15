using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;
    private bool grounded = true;
    private bool pushing = false;
    public Animator anim;


    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        //Running animator
        if (IsMoving()) {
            Vector3 playerScale = this.transform.localScale;
            float scaleX = playerScale.x;
            anim.SetBool("Running", true);
            if(pushing == false) {
                if (rb.velocity.x > 0) {
                    scaleX = 1f;
                }
                else {
                    scaleX = -1f;
                }
            }
            this.transform.localScale = new Vector3(scaleX, playerScale.y, playerScale.z);
        }
        else anim.SetBool("Running", false);

        //Jump animator
        if (rb.velocity.y < 0.5f && !grounded){
            anim.SetBool("Falling", true);
        } else anim.SetBool("Falling", false);

    }


    public void SetPushingAnimation(bool pushing) {
        this.pushing = pushing;
        anim.SetBool("Pushing",pushing);
        return;
    }

    public void SetGroundedAnimation(bool grounded) {
        this.grounded = grounded;
        anim.SetBool("Grounded",grounded);
        return;
    }

    bool IsMoving() {
        return (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f);
    }


}
