using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb; 
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
    public Transform GroundChecker;
    public LayerMask groundLayer;
    public Animator thisAnim;  
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        thisAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if you are within 0.15 position in the Y of the ground
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.35f, groundLayer);
        //Debug.Log("isGrounded: " + isGrounded);
        thisAnim.SetBool("Idle", true);
        Move(); 
        Jump();
        Attack();
        HandleJumping();
    }

    void Move() { 
        float h = Input.GetAxis("Horizontal"); 
        thisAnim.SetFloat("Speed", Mathf.Abs(h));
        float moveBy = h * speed; 
        rb.velocity = new Vector2(moveBy, rb.velocity.y); 
        // This make the character to flip
        if (h < 0.0){
            transform.localScale = new Vector3(-1, 1, 1); 
        } else if (h > 0.0) {
            transform.localScale = new Vector3(1, 1, 1);  
        }

        if (moveBy < 0 && isGrounded || speed > 0 && isGrounded){
        thisAnim.SetInteger("State", 2);
        } 

        if (speed == 0 && isGrounded){
        thisAnim.SetInteger("State", 0);
        }
    }

    void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            thisAnim.SetTrigger("Jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        // Goes higher while key is held
        if (Input.GetKey(KeyCode.Space) && isJumping) {
            if (jumpTimeCounter > 0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }
    }

    // This improves the effect of falling faster
    void HandleJumping() {
        if (!isGrounded) {
            if (rb.velocity.y > 0) {
                thisAnim.SetInteger("State",3);
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
            } else {
                thisAnim.SetInteger("State",1);
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            }
        }
    }

    private void Attack() {
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            thisAnim.SetTrigger("Attack");
        }  
    }
        
}
