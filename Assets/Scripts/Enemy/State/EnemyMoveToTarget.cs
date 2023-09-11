using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToTarget : IState
{
    Enemy enemy;
    public EnemyMoveToTarget(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Enter()
    {
        enemy.CurrentState = Enemy.EnemyState.MoveToTarget;
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        MoveToTarget();
        enemy.CheckEnemySightRang();
        //enemy.CheckEnemyAttackRang();

    }

    public void Tick()
    {
        
    }

    void MoveToTarget()
    {
        Vector2 currentpositin = enemy.rb2d.position;
        Vector2 tatget = enemy.target.GetComponent<Rigidbody2D>().position;
        enemy.transform.position = Vector2.MoveTowards(currentpositin, tatget, enemy.myEnemy.MoveSpeed * Time.fixedDeltaTime);
    }
}
