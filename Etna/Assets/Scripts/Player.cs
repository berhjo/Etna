using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character//inhierit stuff from Character script
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
    
    [SerializeField]
    private float slideSpeed;
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
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    
    // Use this for initialization
    public override void Start()//overide is used so can overide a code that has the same name expet virtual instead of overide. (close???)
    {
        base.Start();//this is used so our Character is calling the star function
        //makes a reference to players Rigidbody
        MyRigibody = GetComponent<Rigidbody2D>();
        //makes a reference to players animator
        
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
            MyAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide && (OnGround || airControll))
        {
            MyRigibody.velocity = new Vector2(horizontal * moveSpeed, MyRigibody.velocity.y);
        }
        if (Jump && MyRigibody.velocity.y == 0)
        {
            MyRigibody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    
    //Handle player inputs and it animations
    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            MyAnimator.SetTrigger("shoot");
        }
    }

    //Flip the player depending on the input
    private void flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirections();
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
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ShootBullet(int value)
    {
        if (!OnGround && value == 0 || OnGround && value ==0)
        {
            base.ShootBullet(value);
        }
    }

}
