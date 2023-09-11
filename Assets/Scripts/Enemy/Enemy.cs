using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    
    [SerializeField] Image hpbar;
    [HideInInspector]
    public Rigidbody2D rb2d;
    public Transform looktarget;


    private void Awake()
    {
        CopyBehaviour();

        idle = new EnemyIdle(this);
        moveTotarget = new EnemyMoveToTarget(this);
        attack = new EnemyAttack(this);
        dead = new EnemyDead(this);

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hpbar = transform.Find("Canvas/HP").GetComponent<Image>();
        looktarget = transform.Find("LookatTarget");
        hpbar.fillAmount = 1;

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
        myEnemy.tagbulletPooling = behaviour.behaviour.tagbulletPooling;
        myEnemy.particle = behaviour.behaviour.particle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            myEnemy.behaviourType = Behaviour.BehaviourType.Agassive;
            Bullet _bullet = bullet.GetComponent<Bullet>();
            if (_bullet.owner.tag != this.gameObject.tag)
            {
                animator.SetTrigger("Hit");
                myEnemy.HP -= _bullet.Darmage;
                target = _bullet.owner;
                ChangeState(moveTotarget);
                bullet.gameObject.SetActive(false);
            }

            hpbar.fillAmount =  ((float)myEnemy.HP/ (float)behaviour.behaviour.HP);

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
                if(myEnemy.behaviourType == Behaviour.BehaviourType.Agassive)
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
                if (myEnemy.behaviourType == Behaviour.BehaviourType.Agassive)
                    ChangeState(moveTotarget);
                return;
            }
        }
        ChangeState(idle);
        return;
    }

    public void lookatTarget()
    {
        Vector3 look = transform.InverseTransformPoint(target.transform.position);
        float Angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
        looktarget.localEulerAngles = new Vector3(0f, 0f, Angle);
    }

    public void DestroythisObject()
    {
        Destroy(gameObject,0.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position - new Vector3(0f, 0.2f, 0f), myEnemy.AttackRang/100);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position - new Vector3(0f,0.2f,0f), myEnemy.SightRang/100);
    }
}
