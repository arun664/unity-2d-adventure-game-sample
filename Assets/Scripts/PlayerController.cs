using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Variables related to player character movement
    [SerializeField] InputActionAsset playerInputController;
    InputActionMap playerInputActionMap;
    InputAction moveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    // Variables related to the health system
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    //Variables related to temporary invicibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    //Variables related to animation
    Animator animator;
    Vector2 moveDirection = new Vector2 (1, 0);

    private void Awake()
    {
        playerInputActionMap = playerInputController.FindActionMap("PlayerMovement");
        moveAction = playerInputController.FindAction("Movement");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        //Debug.Log(move);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();

        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible )
        {
            damageCooldown -= Time.deltaTime;
            if(damageCooldown < 0 )
            {
                isInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if(isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        animator.SetTrigger("Hit");
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }
}
