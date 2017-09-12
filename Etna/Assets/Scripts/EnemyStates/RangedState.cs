using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    private float throwTimer;
    private float throwCoolDown = 3;
    private bool canThrow;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowKnife();
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
    private void ThrowKnife()
    {
        throwTimer += Time.deltaTime;
        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;//resets the timer if true
        }

        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnimator.SetTrigger("shoot");
        }
    }
}
