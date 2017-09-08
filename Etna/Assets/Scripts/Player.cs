using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    
    [SerializeField]
    private float moveSpeed;
    private bool attack;
    private bool slide;
    private bool facingRight;

    // Use this for initialization
    void Start ()
    {
        facingRight = true;//makes player allways facing right at start of the game
        //makes a reference to players Rigidbody
        myRigidbody = GetComponent<Rigidbody2D>();
        //makes a reference to players animator
        myAnimator = GetComponent<Animator>();
	}
    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    //FixedUpate is used to prevent better pc from having higher speed (timestep)
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        handelMovement(horizontal);
        flip(horizontal);
        handleAttack();
        resetValues();
	}
    //Hanle player movement and its animation
    private void handelMovement(float horizontal)
    {
        if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))//Prevent player from moving if a animation with the tag Attack is active
        {
            myRigidbody.velocity = new Vector2(horizontal * moveSpeed, myRigidbody.velocity.y);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));//mathf.abs is used so we dont return a negative value

        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
    }

    //Handle player attack and its animation
    private void handleAttack()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))//prevent player from attack until the animation is finished
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;//stops the player movement while attacking
        }
    }

    //Handle player inputs and it animations
    private void handleInput()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            attack = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            slide = true;
        }
    }

    //Flip the player depending on the input
    private void flip(float horizontal)
    {
        if(horizontal>0 && !facingRight || horizontal<0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    //Reset values function
    private void resetValues()
    {
        attack = false;
        slide = false;
    }
}
