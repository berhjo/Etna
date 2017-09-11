using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Ranged");
        if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTrigger(Collider2D other)
    {

    }

}
