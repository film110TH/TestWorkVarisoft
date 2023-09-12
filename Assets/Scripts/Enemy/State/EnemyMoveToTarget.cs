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
        enemy.animator.SetBool("Run",true);
        enemy.CurrentState = Enemy.EnemyState.MoveToTarget;
        timeToHunt = 10;
    }

    public void Exit()
    {
        enemy.animator.SetBool("Run", false);
    }

    public void FixedTick()
    {
        MoveToTarget();

        if (timeToHunt > 0)
            Timetohunt();
        else { enemy.CheckEnemySightRang();}

        enemy.lookatTarget();
    }

    public void Tick()
    {
        
    }

    public void Timetohunt()
    {
        timeToHunt -= Time.deltaTime;

        Collider2D[] AttackRang = Physics2D.OverlapCircleAll(enemy.rb2d.position - new Vector2(0f, 0.2f), enemy.myEnemy.SightRang / 100);
        foreach (Collider2D collider2D in AttackRang)
        {
            if (collider2D.TryGetComponent<PlayerController>(out PlayerController player))
            {
                timeToHunt = 0;
            }
        }
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
