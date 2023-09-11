using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy" , menuName ="Enemy/New Enemy")]
public class EnemyBehaviour : ScriptableObject
{
    public Behaviour behaviour;
}

[Serializable]
public class Behaviour
{
    public int HP;
    public int Damage;
    public int AttackSpeed;
    public int MoveSpeed;

    public float SightRang;
    public float AttackRang;

    public enum AttackType { Melee, Range }
    public AttackType attackType;

    public enum BehaviourType { Passpassive , Agassive }
    public BehaviourType behaviourType;

    public string tagbulletPooling;
    public GameObject particle;
}
