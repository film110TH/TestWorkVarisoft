using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToTarget : IState
{
    Enemy enemy;
    float timeToHunt;

    public EnemyMoveToTarget(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Enter()
    {
        enemy.CurrentState = Enemy.EnemyState.MoveToTarget;
        timeToHunt = 10;
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        MoveToTarget();
        enemy.CheckEnemySightRang();
        enemy.lookatTarget();
    }

    public void Tick()
    {
        
    }

    void MoveToTarget()
    {
        Vector2 currentpositin = enemy.rb2d.position;
        Vector2 tatget = enemy.target.GetComponent<Rigidbody2D>().position;

        if (currentpositin.x - tatget.x > 0)
            enemy.GetComponent<SpriteRenderer>().flipX = true;
        else { enemy.GetComponent<SpriteRenderer>().flipX = false; }

        enemy.transform.position = Vector2.MoveTowards(currentpositin, tatget, enemy.myEnemy.MoveSpeed * Time.fixedDeltaTime);
    }
}
