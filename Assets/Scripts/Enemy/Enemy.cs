using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : StateMachine
{
    public enum EnemyState {Idle , MoveToTarget , Attack , Dead }
    public EnemyState CurrentState;

    public EnemyIdle idle { get; private set; }
    public EnemyMoveToTarget moveTotarget { get; private set; }
    public EnemyAttack attack { get; private set; }
    public EnemyDead dead { get; private set; }

    public EnemyBehaviour behaviour;
    public Behaviour myEnemy;
    public Animator animator;
    public GameObject target;

    [HideInInspector]
    public Rigidbody2D rb2d;
    private void Awake()
    {
        CopyBehaviour();

        idle = new EnemyIdle(this);
        moveTotarget = new EnemyMoveToTarget(this);
        attack = new EnemyAttack(this);
        dead = new EnemyDead(this);

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ChangeState(idle);
    }

    void CopyBehaviour()
    {
        myEnemy = new Behaviour();

        myEnemy.HP = behaviour.behaviour.HP;
        myEnemy.Damage = behaviour.behaviour.Damage;
        myEnemy.AttackSpeed = behaviour.behaviour.AttackSpeed;
        myEnemy.MoveSpeed = behaviour.behaviour.MoveSpeed;
        myEnemy.SightRang = behaviour.behaviour.SightRang;
        myEnemy.AttackRang = behaviour.behaviour.AttackRang;
        myEnemy.attackType = behaviour.behaviour.attackType;
        myEnemy.behaviourType= behaviour.behaviour.behaviourType;
        myEnemy.particle = behaviour.behaviour.particle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            myEnemy.HP -= 5;
            collision.gameObject.SetActive(false);
            if(myEnemy.HP <= 0)
                ChangeState(dead);
        }
    }

    public void CheckEnemySightRang()
    {
        Collider2D[] AttackRang = Physics2D.OverlapCircleAll(rb2d.position - new Vector2(0f, 0.2f), myEnemy.AttackRang / 100);
        foreach (Collider2D collider2D in AttackRang)
        {
            if (collider2D.TryGetComponent<PlayerController>(out PlayerController player))
            {
                target = player.gameObject;
                ChangeState(attack);
                return;
            }
        }

        Collider2D[] SightRang = Physics2D.OverlapCircleAll(rb2d.position - new Vector2(0f,0.2f), myEnemy.SightRang / 100);
        foreach(Collider2D collider2D in SightRang)
        {
            if (collider2D.TryGetComponent<PlayerController>(out PlayerController player))
            {
                target = player.gameObject;
                ChangeState(moveTotarget);
                return;
            }
        }
        ChangeState(idle);
        return;
    }

    public void DestroythisObject()
    {
        Destroy(gameObject,1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position - new Vector3(0f, 0.2f, 0f), myEnemy.AttackRang/100);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position - new Vector3(0f,0.2f,0f), myEnemy.SightRang/100);
    }
}
