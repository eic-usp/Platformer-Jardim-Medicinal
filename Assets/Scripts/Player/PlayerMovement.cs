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
    
    public PlayerAnimationController anim;
    public LayerMask interactableMask;

    [Header("Stats")]
    public float speed = 10;
    public float pushingSpeed = 3;
    public float jumpVelocity = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float interactDistance = 1f;


    [Header("Bools")]
    private bool  canMove;
    private bool pushing;
    private bool grounded;

    public bool Pushing {
        get => pushing;
        set {
            pushing = value;
            anim.SetPushingAnimation(pushing);
            speed = (pushing) ? pushingSpeed : 10;

        }
    }

    public bool Grounded {
        get => grounded;
        set {
            grounded = value;
            anim.SetGroundedAnimation(grounded);
        }
    }

    public bool CanMove{
        get => canMove;
        set {
            canMove = value;
            MovementConstraints(!value);
        }
    }
    


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        feet = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        anim = GetComponent<PlayerAnimationController>();
        groundLayers = LayerMask.GetMask("Ground");
        CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate() {

        isGrounded();
        
        if(CanMove) {
            Vector2 dir = GetInputs();

            //clear raycast to push
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right * transform.localScale.x,interactDistance,interactableMask);

            Move(dir);
            

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

            //interact
            if(Input.GetKeyDown(KeyCode.E)) {
                if (hit.collider != null && hit.collider.gameObject.tag == "Interactable") {
                    Interact(hit);
                }
            }            
        }

        /*if(Input.GetKeyDown(KeyCode.Q)) {
            CanMove = !CanMove;
        }*/
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
        Grounded = (groundCheck != null) ? true : false;
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

    void MovementConstraints(bool on) {
        if(on) {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }else {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
