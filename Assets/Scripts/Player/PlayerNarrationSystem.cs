using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNarrationSystem : MonoBehaviour , IObserver
{
    private Subject _playerSubject;
    private PlayerActionController _controller;
    

    private void Awake()
    {
        _playerSubject = GetComponentInChildren<Subject>();
        _controller = GetComponent<PlayerActionController>();
    }
    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.Attack:
                {
                    _controller.OnAttack(_playerSubject.gameObject.transform);
                }
                break;
            case PlayerAction.TakeDamage:
                {
                    _controller.OnPlayerTakeDamage();
                }
                break;
        }
    }

    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
