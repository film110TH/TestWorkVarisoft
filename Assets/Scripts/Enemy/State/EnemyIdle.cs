using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class EnemyIdle : IState
{
    Enemy enemy;
    Vector2 randompo;

    bool walkable = true;
    bool idle = true;
    public EnemyIdle(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Enter()
    {
        enemy.CurrentState = Enemy.EnemyState.Idle;
        randomPosition();
        //enemy.Invoke(nameof(randompo), 3f);
    }

    public void Exit()
    {
        
    }

    public  void FixedTick()
    {
        EnemyMovibg();
        enemy.CheckEnemySightRang();
    }

    public void Tick()
    {

    }

    private async void EnemyMovibg()
    {
        if (walkable)
        {
            randomPosition();

            walkable = false;
            walkable = await Tool.Delaybool(walkable, 4f);
        }
        Vector2 currentpositin = enemy.rb2d.position;
        Vector2 targetpositon = currentpositin + randompo * enemy.myEnemy.MoveSpeed * Time.fixedDeltaTime;
        enemy.rb2d.MovePosition(targetpositon);
    }

    public void randomPosition()
    {
        randompo = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        randompo.Normalize();
        Tool.Delayfuntion(() => { randompo = Vector2.zero; }, 2f);
    }
}
