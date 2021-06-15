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
    
    //public PlayerAnimationController anim;

    [Header("Stats")]
    public float speed = 10;
    public float remainingFuel = 10f;
    public float holdTimer;

    [Header("Bools")]
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
        groundLayers = LayerMask.GetMask("Ground");
        ChangeModeTo(WITHPLAYER);
    }

    // Update is called once per frame
    void Update() {
        if(mode == WITHPLAYER) {
            Debug.Log("update WITHPLAYER");
            this.transform.position = player.GetComponent<Transform>().position;
            Debug.Log("Chagned position to player");
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
                //try to connect
            }
        }

        DetectHoldButton();



    }

    private Vector2 GetInputs() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        return (new Vector2(x, y));
    }

    private void Move(Vector2 dir) {
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }


    void MovementConstraints(bool on) {
        if(on) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }else {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    //TODO
    //Change to recive keycode as argument and maybe a float too?
    void DetectHoldButton() {
        if(Input.GetKey(KeyCode.Q)) {
            holdTimer += Time.deltaTime;
            if(holdTimer > 2f) {
                this.rb.simulated = false;
                ChangeModeTo(WITHPLAYER);
                holdTimer = 0f;
            }
        }else holdTimer = 0f;
    }
    
    public void ChangeModeTo(int m) {
        if(m == WITHPLAYER) {
            this.rb.simulated = false;
            this.mode = WITHPLAYER;
            CanMove = false;
            MovementConstraints(true);
        }
        else if(m == FLYING) {
            this.rb.simulated = true;
            this.rb.gravityScale = 0;
            this.mode = FLYING;
            CanMove = true;
            MovementConstraints(false);
        }
        else if(m == ONHOLD) {
            this.mode = ONHOLD;
            CanMove = false;
            MovementConstraints(true);
        }
        else if(m == DISCONNECTED) {
            this.mode = DISCONNECTED;
            this.rb.simulated = true;
            CanMove = false;
            MovementConstraints(false);
            this.rb.gravityScale = 2;
        }
    }
}
