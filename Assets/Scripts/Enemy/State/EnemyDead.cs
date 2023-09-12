using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : IState
{
    Enemy enemy;
    public EnemyDead(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Enter()
    {
        enemy.CurrentState = Enemy.EnemyState.Dead;
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.animator.SetTrigger("Death");
        enemy.DestroythisObject();
        EnemySpawner.Instance.removeEnemyObject(enemy.gameObject);
        InGameMenuManager.instance.addScore(enemy.myEnemy.Score);
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}
