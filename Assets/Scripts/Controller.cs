using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

public class Controller : MonoBehaviour {


    public float moveSpeed;
    public float jumpForce;
	public int coin;
	public bool facingRight = true;
	private Rigidbody2D rb;
    private int directionInput;
    private Vector3 target;
    private Vector3 direction;
    private bool isGrounded = false;
	private Animator animator;


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    


	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	
	void Update ()
	{
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

    public void Move(int InputAxis)
	{
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

	public void ReceiveDamage()
	{
		rb.velocity = Vector3.zero;
		rb.AddForce (transform.up * 6.0f, ForceMode2D.Impulse);
		rb.AddForce (transform.right * -6.0f, ForceMode2D.Impulse);
	}

	public void ReceiveForce(float force)
	{
		rb.velocity = Vector3.zero;
		rb.AddForce (transform.up * force, ForceMode2D.Impulse);

	}

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;
    }

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.name == "Coin") 
		{
			Destroy (collider.gameObject); 
			coin++;
		}
	}

	public void OnGUI()
	{
		GUI.Box (new Rect(0,0,100,20), "Coins: " + coin);
	}

}




	public enum CharState
	{
	    Idle,
	    Walk,
	    Jump
	}