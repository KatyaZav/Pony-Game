using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private enum Special
    {
        DoubleJump = 1,
        Soaring = 2,
        Atack = 3,
        Telekinez = 4
    } 

    public float speed;
    public float runSpeed;
    public float jumpforce;
    private float moveInput;

    private bool isRun = false;
    private bool isMagicActive = false;

    private int JumpsCounts = 2;
    private int MaxJumpCounts = 2;
    private bool canJump = true;

    private bool isDoubleJumpActive = true;
    private bool isSoaring = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool facingRight = false;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMagicActive) { }
        else
        {
            anim.SetBool("DoMagic", false);
            DoMove();               
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMagicActive)
            {
                isMagicActive = false;
                anim.SetBool("DoMagic", false);
            }
            else
            {
                isMagicActive = true;
                anim.SetBool("MagicOn", true);
                anim.SetBool("DoMagic", true);
            }
        }
        if (isMagicActive) { }
        else
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            
            if (isGrounded)
            {
                JumpsCounts = MaxJumpCounts;
                anim.SetBool("isJumping", false);                
            }
            else
            {
                anim.SetBool("isJumping", true);
            }
            if (canJump)
            {
                if (JumpsCounts > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)
                    ))//|| Input.GetKeyDown(KeyCode.Space)))
                {
                    Jump();
                }
            }

            if (isSoaring && Input.GetKey(KeyCode.Space) && !isGrounded)
            {
                anim.SetBool("WingsOn", true);
                anim.SetBool("IsSoaring", true);
                rb.gravityScale = 1;
                rb.drag = 5;
                canJump = false;
            }
            else
            {
                anim.SetBool("IsSoaring", false);
                rb.gravityScale = 5;
                rb.drag = 0;
                canJump = true;
            }

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Jump()
    {
        JumpsCounts--;  

        if (JumpsCounts == MaxJumpCounts-1 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpforce;
            anim.SetBool("TakeOff", true);            
        }
        else if (JumpsCounts == 0 && !isGrounded)
        {
            rb.velocity = Vector2.up * jumpforce;
            anim.SetBool("TakeOff", true);
            anim.SetBool("WingsOn", true);
            anim.SetBool("DoubleJumpOn", true);
        }

        if (JumpsCounts > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)
                    ))//|| Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }
    }

    void DoMove()
    {
        float spd;
        moveInput = Input.GetAxis("Horizontal");
        if (isRun)
            spd = runSpeed;
        else spd = speed;
        rb.velocity = new Vector2(spd * moveInput, rb.velocity.y);
        if ((facingRight == true && moveInput < 0) || (facingRight == false && moveInput > 0))
        {
            Flip();
        }

        isRun = Input.GetKey(KeyCode.LeftControl);
        anim.SetBool("isRunning", isRun);
        anim.SetBool("isWalk", !(moveInput == 0));
    }
}
