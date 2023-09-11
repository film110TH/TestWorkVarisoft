using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Subject
{
    public static PlayerController instance;

    public float hp;
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

        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
}
