using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();//only works if their is 1 player/object the the game
            }
            return instance;
        }

    }

    private Animator myAnimator;
    [SerializeField]//Makes private float moveSpeed viseble in the inspector
    private float moveSpeed;
    [SerializeField]
    private float slideSpeed;
    private bool facingRight;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;//indicate what ground is to player
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool airControll;
    public Rigidbody2D MyRigibody { get; set; }
    public bool Attack { get; set; }
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    
    // Use this for initialization
    void Start()
    {
        facingRight = true;//makes player allways facing right at start of the game
        //makes a reference to players Rigidbody
        MyRigibody = GetComponent<Rigidbody2D>();
        //makes a reference to players animator
        myAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    //FixedUpate is used to prevent better pc from having higher speed (timestep)
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();//is used so we don't have to call Isgrounded functions multiple times per fixedupdate
        handelMovement(horizontal);
        flip(horizontal);
        handleLayers();
    }
    //Hanle player movement and its animation
    private void handelMovement(float horizontal)
    {
        if (MyRigibody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide && (OnGround || airControll))
        {
            MyRigibody.velocity = new Vector2(horizontal * moveSpeed, MyRigibody.velocity.y);
        }
        if (Jump && MyRigibody.velocity.y == 0)
        {
            MyRigibody.AddForce(new Vector2(0, jumpForce));
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    
    //Handle player inputs and it animations
    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            myAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnimator.SetTrigger("shoot");
        }
    }

    //Flip the player depending on the input
    private void flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private bool IsGrounded()
    {
        if (MyRigibody.velocity.y <= 0)//checks if player is standing still or falling down and then checks if player is grounded, 0 player idle, less then 0 player is falling
        {
            foreach (Transform point in groundPoints)//checks if any of the groundPoinis is colliding with something
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);//creates collider for groundpoints
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;//returns true is player collids with something else then the player
                    }
                }
            }
        }
        return false;
    }

    private void handleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
