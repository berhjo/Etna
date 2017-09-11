using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;//this is the time enemy has been in idle
    private float idleDuration = 5;//this is the time enemy will stay in idle

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Idle");
        Idle();

        if (enemy.Target != null)//if enemy is in idleState and Player is in range, changes state to patrolState
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {

    }

    public void OnTrigger(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
