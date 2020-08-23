using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb; 
    float RotateX;
    float RotateY;
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
    public Transform GroundChecker; // Put the prefab of the ground here
    public LayerMask groundLayer; // Insert the layer here.
    public Animator thisAnim;  

    // Start is called before the first frame update
    void Start()
    {
        thisAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.15f, groundLayer); // checks if you are within 0.15 position in the Y of the ground
        Debug.Log("isGrounded: " + isGrounded);
        thisAnim.SetBool("Idle", true);
        Move(); 
        Jump();
        Attack();
    }

    void Move() { 
        float h = Input.GetAxis("Horizontal"); 
        thisAnim.SetFloat("Speed", Mathf.Abs(h));
        float moveBy = h * speed; 
        rb.velocity = new Vector2(moveBy, rb.velocity.y); 
        if (h < 0.0){
            transform.localScale = new Vector3(-1, 1, 1); 
        } else if (h > 0.0) {
            transform.localScale = new Vector3(1, 1, 1);  
        }
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            thisAnim.SetTrigger("Jump");
            Debug.Log("Jump");
        } 
    }

    private void Attack() {
        if(Input.GetKey(KeyCode.Mouse0)){
            thisAnim.SetTrigger("Attack");
            Debug.Log("attack");
        }
       
    }
        
}
