using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(Enemy enemy);//mayby enemyninja???
    void Exit();
    void OnTrigger(Collider2D other);
	
}
