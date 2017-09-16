using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;//this is the time enemy has been in patrol
    private float patrolDuration = 10;//this is the time enemy will stay in patrol

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Patrol");
        Patrol();
        enemy.Move();
        
        if (enemy.transform != null)//if enemy is in patrolState and Player is in range, changes state to rangedState
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTrigger(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirections();
        }
    }

    private void Patrol()
    {
        
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
