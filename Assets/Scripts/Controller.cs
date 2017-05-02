using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public int directionInput;
    private Vector3 target;
    public Vector3 direction;
    private bool isGrounded = false;
    private Animator animator;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    public bool facingRight = true;


	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (isGrounded) State = CharState.Idle;
        if (!isGrounded) State = CharState.Jump;



        if ((directionInput < 0) && (facingRight))
        {
            Flip();
        }

        if((directionInput > 0) && (!facingRight))
        {
            Flip();
        }

        Run();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    public void Move(int InputAxis) {
        directionInput = InputAxis;
        direction = transform.right * directionInput; 
    }

    public void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        if((directionInput != 0) &&(isGrounded)) State = CharState.Walk;
    }

    public void Jump(bool Jump)
    {
       if(isGrounded) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;
    }

}

public enum CharState
{
    Idle,
    Walk,
    Jump
}