using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IState
{
    Enemy enemy;
    public EnemyAttack(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Enter()
    {
        enemy.CurrentState = Enemy.EnemyState.Attack;
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        Attack();
        enemy.CheckEnemySightRang();
    }

    public void Tick()
    {
        
    }

    bool attackable = true;

    private async void Attack()
    {
        if (!attackable)
            return;

        enemy.animator.SetTrigger("Attack");

        attackable= false;
        attackable = await Tool.Delaybool(attackable, enemy.myEnemy.AttackSpeed);
    }
}
