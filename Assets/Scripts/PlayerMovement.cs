using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;
    private Transform feet;
    private LayerMask groundLayers;
    private float x;
    private float y;
    
    public Animator anim;
    public LayerMask interactableMask;
    GameObject box;

    [Header("Stats")]
    public float speed = 10;
    public float pushingSpeed = 3;
    public float jumpVelocity = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float interactDistance = 1f;


    [Header("Bools")]
    private bool pushing;
    private bool grounded;

    public bool Pushing {
        get => pushing;
        set {
            pushing = value;
            anim.SetBool("Pushing",pushing);
            speed = (pushing) ? pushingSpeed : 10;

        }
    }

    public bool Grounded {
        get => grounded;
        set {
            grounded = value;
            anim.SetBool("Grounded",grounded);
        }
    }
    


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        feet = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        anim = GetComponent<Animator>();
        groundLayers = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update() {

        Vector2 dir = GetInputs();

        //clear raycast to push
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * transform.localScale.x,interactDistance,interactableMask);

        Move(dir);
        isGrounded();

        if (Input.GetButtonDown("Jump") && Grounded) {
            Jump();
        }

        //Better jump gravity modifiers
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Running animator
        if (rb.velocity.x != 0) {
            Vector3 playerScale = this.transform.localScale;
            float scaleX = playerScale.x;
            anim.SetBool("Running", true);
            if(Pushing == false) {
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

        //box push
        if(Input.GetKeyUp(KeyCode.E)) {
            if (hit.collider != null && hit.collider.gameObject.tag == "Interactable") {
                Interact(hit);
            }
        }
    }

    private Vector2 GetInputs() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        return (new Vector2(x, y));
    }

    private void Move(Vector2 dir) {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Jump() {
        rb.velocity = Vector2.up * jumpVelocity;
    }

    public bool isGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
        if (groundCheck != null)
            Grounded = true;
        else {
            Grounded = false;
        }
        return(Grounded);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,(Vector2) transform.position + Vector2.right * transform.localScale.x * interactDistance);
    }

    private void Interact(RaycastHit2D hit) {
        GameObject obj = hit.collider.gameObject;
        obj.GetComponent<Interactable>().OnInteract();
    }
}
