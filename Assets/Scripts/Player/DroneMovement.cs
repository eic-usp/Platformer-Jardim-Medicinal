using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;
    private GameObject player;
    private Transform feet;
    private LayerMask groundLayers;
    private float x;
    private float y;

    //Drone modes 
    public int mode;
    static int WITHPLAYER = 0;
    static int FLYING = 1;
    static int ONHOLD = 2;
    static int DISCONNECTED = 3;
    
    public Animator anim;

    [Header("Stats")]
    public float speed = 10;
    public float remainingFuel = 10f;
    public float holdTimer;

    [Header("Bools")]
    private bool onFlyingZone;
    private bool  canMove;


    public bool CanMove{
        get => canMove;
        set {
            canMove = value;
            player.GetComponent<PlayerMovement>().CanMove = !value;
        }
    }
    


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        feet = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        player = GameObject.Find("PlayerSprite");
        anim = player.GetComponent<Animator>();
        groundLayers = LayerMask.GetMask("Ground");
        onFlyingZone = true;
        ChangeModeTo(WITHPLAYER);
    }

    // Update is called once per frame
    void Update() {
        if(mode == WITHPLAYER) {
            this.transform.position = player.GetComponent<Transform>().position;
            if(Input.GetKeyDown(KeyCode.Q)) {
                ChangeModeTo(FLYING);
            }
        }
        else if(mode == FLYING) {
            Vector2 dir = GetInputs();
            Move(dir);
            if(Input.GetKeyDown(KeyCode.Q)) {
                ChangeModeTo(ONHOLD);
            }
        }
        else if(mode == ONHOLD) {
            if(Input.GetKeyDown(KeyCode.Q)) {
                ChangeModeTo(FLYING);
            }
        }
        else if(mode == DISCONNECTED) {
            if(Input.GetKeyDown(KeyCode.Q)) {
                if(onFlyingZone) {
                    ChangeModeTo(FLYING);
                }
            }
        }

        if(DetectHoldButton(KeyCode.Q, 2f)) {//change 2f to a variable later
            ChangeModeTo(WITHPLAYER);
        }



    }

    private Vector2 GetInputs() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        return (new Vector2(x, y));
    }

    private void Move(Vector2 dir) {
        if(HasPlayerOnTop() && dir.y > 0) dir.y = 0f;
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }


    bool DetectHoldButton(KeyCode action, float time) {
        if(Input.GetKey(action)) {
            holdTimer += Time.deltaTime;
            if(holdTimer > time) {
                this.rb.simulated = false;
                holdTimer = 0f;
                return true;
            }
        }else holdTimer = 0f;
        return false;
    }
    
    public void ChangeModeTo(int m) {
        if(m == WITHPLAYER) {
            anim.SetBool("UsingDrone", false);
            this.rb.simulated = false;
            this.mode = WITHPLAYER;
            CanMove = false;
            MovementConstraints(true);
        }
        else if(m == FLYING) {
            anim.SetBool("UsingDrone", true);
            this.rb.simulated = true;
            this.rb.gravityScale = 0;
            this.mode = FLYING;
            CanMove = true;
            MovementConstraints(false);
        }
        else if(m == ONHOLD) {
            anim.SetBool("UsingDrone", false);
            this.mode = ONHOLD;
            CanMove = false;
            MovementConstraints(true);
        }
        else if(m == DISCONNECTED) {
            anim.SetBool("UsingDrone", false);
            this.mode = DISCONNECTED;
            this.rb.simulated = true;
            CanMove = false;
            MovementConstraints(false);
            this.rb.gravityScale = 2;
        }
    }


    bool HasPlayerOnTop() {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.up * transform.localScale.x, 1f, LayerMask.GetMask("Player"));
        if(hit.rigidbody != null && hit.rigidbody.tag == "Player") {
            return(true);
        }
        else return (false);
    }

    void MovementConstraints(bool on) {
        if(on) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }else {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void SetOnFlyingZone(bool value) {
        onFlyingZone = value;
    }
}
