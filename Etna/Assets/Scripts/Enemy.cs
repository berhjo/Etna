using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;//this is the enemy current state

    public GameObject Target { get; set; }

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());//sets the current state to idle, at start
	}

    
    private void LookAtTarget()//Looks were the target is
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirections();
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentState.Execute();
        LookAtTarget();
	}

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);
        transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
    }
    //Get's the dircetion player is facing
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;//this is a if-statement
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTrigger(other);
    }
}
