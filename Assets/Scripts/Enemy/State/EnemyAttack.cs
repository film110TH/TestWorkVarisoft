using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        enemy.lookatTarget();

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

        switch (enemy.myEnemy.attackType)
        {
            case Behaviour.AttackType.Melee:
                {
                    Collider2D[] AttackRang = Physics2D.OverlapCircleAll(enemy.rb2d.position - new Vector2(0f, 0.2f),enemy.myEnemy.AttackRang + 50 / 100);
                    foreach (Collider2D collider2D in AttackRang)
                    {
                        if (collider2D.TryGetComponent<PlayerController>(out PlayerController player))
                        {
                            player.TakeDamage(enemy.myEnemy.Damage);
                        }
                    }
                }
                break;
            case Behaviour.AttackType.Range:
                {
                    Debug.Log("Attack : " + Behaviour.AttackType.Range);
                    GameObject _bullet = Objectpooler.instance.SpawnFrompool(enemy.myEnemy.tagbulletPooling, enemy.transform.position, enemy.looktarget.rotation);
                    Bullet AddOwner = _bullet.GetComponent<Bullet>();
                    AddOwner.locktraget = true;
                    AddOwner.owner = enemy.gameObject;
                    AddOwner.Darmage = enemy.myEnemy.Damage;
                }
                break;
        }
        attackable= false;
        attackable = await Tool.Delaybool(attackable, enemy.myEnemy.AttackSpeed);
    }
}