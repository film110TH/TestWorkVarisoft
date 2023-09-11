using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    bool Isfire = true;
    PlayerController playercontroller;
    private void Awake()
    {
        playercontroller = GetComponentInChildren<PlayerController>();
    }
    public async void OnAttack(Transform PlayerPo)
    {
        if (!Isfire)
            return;

        GameObject _bullet = Objectpooler.instance.SpawnFrompool("Bullet", PlayerPo.position, Quaternion.identity);
        Rigidbody2D rb2d = _bullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = AttackPoint.getAttackPoint(IsometricCharacterRenderer.lastDirection) * 10;
        Tool.SetActive(_bullet, false, 3);

        Bullet AddOwner = _bullet.GetComponent<Bullet>();
        AddOwner.owner = playercontroller.gameObject;
        AddOwner.Darmage = playercontroller.damage;

        Isfire = false;
        Isfire = await Tool.Delaybool(Isfire, playercontroller.fireRate);
    }

    public void OnPlayerTakeDamage()
    {
        Debug.Log("PlayerTakeDamage");
    }
}
