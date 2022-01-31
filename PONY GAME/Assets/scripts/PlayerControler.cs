using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public float jumpforce;
    private float moveInput;

    private bool isRun = false;
    private bool isMagicActive = false;
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

            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("isRunning", true);
                isRun = true;
            }
            else
            {
                anim.SetBool("isRunning", false);
                isRun = false;
            }

            if (moveInput == 0)
            {
                anim.SetBool("isWalk", false);
            }
            else
            {
                anim.SetBool("isWalk", true);
            }
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        /*if (isRun == false && Input.GetKeyDown(KeyCode.LeftControl))
            isRun = true;
        else if (isRun == true && !Input.GetKeyDown(KeyCode.LeftControl))
            isRun = false;*/

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
            if (isGrounded == true && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) 
                || Input.GetKeyDown(KeyCode.Space)))
            {
                rb.velocity = Vector2.up * jumpforce;
                anim.SetBool("TakeOff", true);
            }
            if (isGrounded == true)
            {
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping", true);
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
}
