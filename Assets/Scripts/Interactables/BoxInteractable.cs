using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractable : MonoBehaviour, Interactable{

	public float defaultMass = 5f;
	public float imovableMass = 500f;

	public bool BeingPushed{
        get => beingPushed;
        set {
            beingPushed = value;
            GetComponent<Rigidbody2D>().mass = (beingPushed) ? defaultMass : imovableMass;
        }
    }
	
	private bool beingPushed;
	private Transform bottomLeft;
    private Transform bottomRight;
    private LayerMask groundLayers;

	void Start() {
		bottomLeft = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        bottomRight = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        GetComponent<Rigidbody2D>().mass = imovableMass;
        groundLayers = LayerMask.GetMask("Ground");
	}

    void Update() {
        if(beingPushed){
            if(!isGrounded() || !IsPlayerGrounded()) Release();
        }
    }

    public void OnInteract() {
        if(!BeingPushed) Grab();
        else Release();
    }

    void Grab() {
        GameObject player = GameObject.Find("PlayerSprite");
        this.GetComponent<FixedJoint2D>().enabled = true;
        this.GetComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        BeingPushed = true;
        player.GetComponent<PlayerMovement>().Pushing = true;
    }

    void Release() {
        GameObject player = GameObject.Find("PlayerSprite");
        this.GetComponent<FixedJoint2D>().enabled = false;
        player.GetComponent<PlayerMovement>().Pushing = false;
        BeingPushed = false;
    }

	public bool isGrounded() {
        Collider2D groundCheckLeft = Physics2D.OverlapCircle(bottomLeft.position, 0.5f, groundLayers);
        Collider2D groundCheckRight = Physics2D.OverlapCircle(bottomRight.position, 0.5f, groundLayers);
        return ((groundCheckLeft == null) && (groundCheckRight == null) ? false : true);
    }

    private bool IsPlayerGrounded() {
        GameObject player = GameObject.Find("PlayerSprite");
        return(player.GetComponent<PlayerMovement>().isGrounded());
    }
}