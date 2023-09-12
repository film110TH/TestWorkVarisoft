using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class PlayerController : Subject
{
    public static PlayerController instance;

    public float hp;
    [HideInInspector]
    public float maxHp;
    public int damage; 
    public float fireRate;
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    public PlayerInput playerInput;

    Rigidbody2D rbody;

    

    private void Awake()
    {
        if(instance == null)
            instance = this;

        maxHp = hp;
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions["Attack"].IsPressed())
            NoitfyObserver(PlayerAction.Attack);
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 InputMove = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 currentPos = rbody.position;

        Vector2 inputVector = new Vector2(InputMove.x, InputMove.y);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Bullet _bullet = bullet.GetComponent<Bullet>();
            if (_bullet.owner.tag != this.gameObject.tag)
            {
                bullet.gameObject.SetActive(false);
                TakeDamage(_bullet.Darmage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        NoitfyObserver(PlayerAction.TakeDamage);

        if(hp <= 0)
        {
            InGameMenuManager.instance.GameOver();
            Debug.Log("Dead");
        }
    }
}
