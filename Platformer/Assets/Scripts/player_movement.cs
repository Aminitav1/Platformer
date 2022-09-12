using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public ParticleSystem jump;
    public ParticleSystem run;
    public ParticleSystem dash;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    //Initialise some variables
    private float dirX = 0f;
    private float dirY = 0f;
    private float dashX = 0f;
    private float dashY = 0f;
    private float dashs = 1f;
    private int frames = 0;

    [SerializeField] private float move_speed = 8f;
    [SerializeField] private float jump_force = 14f;
    [SerializeField] private float dash_force = 0.1f;
    [SerializeField] private float decay_rate = 0.975f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSFX;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
            frames = frames - 1;

            //Check movement
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");

            if(IsGrounded()) {
                dashs = 1;
                if(Mathf.Abs(dirX)>0.5){
                    CreateRunDust();
                }
            }

            if(frames > 0) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            //Sideways movement
            rb.velocity = new Vector2(dirX*move_speed, rb.velocity.y);

            //Jump
            if (Input.GetButtonDown("Jump") && IsGrounded() ) {
                rb.velocity = new Vector2(rb.velocity.x, jump_force);
                jumpSFX.Play();
                CreateJumpDust();
            }

            //Gives variable jump height
            if (Input.GetButtonUp("Jump") & rb.velocity.y > 0) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
            }
            if (Input.GetButtonDown("Dash") && dashs != 0f) {
                dashX = dirX*dash_force;
                dashY = dirY*dash_force;
                jumpSFX.Play();
                dashs = dashs - 1f;
                //rb.velocity = new Vector2(dashX, dashY);
                frames = 20;
                CreateDashDust();
            }
            transform.position = new Vector2(transform.position.x+dashX, transform.position.y+dashY);

            
            dashX = dashX*decay_rate;
            dashY = dashY*decay_rate;

            if (Mathf.Abs(dashX) < 0.01) {
                dashX = 0;
            }
            if (Mathf.Abs(dashY) < 0.01) {
                dashY = 0;
            }

            Debug.Log(dirX);

        UpdateAnimation();
    }

    private void UpdateAnimation() {
        MovementState state;

        if (dirX > 0f) {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else {
            state = MovementState.idle;
            //sprite.flipX = false;
        }

        if (rb.velocity.y > .1f) {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void CreateJumpDust() {
        jump.Play();
    }

    void CreateRunDust() {
        run.Play();
    }

    void CreateDashDust() {
        dash.Play();
    }
} 
