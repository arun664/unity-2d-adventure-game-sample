#define COMMENT_REGION
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
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

    //Variables for Projectile
    public GameObject projectilePrefab;
    InputAction launchAction;

    //Variables for NPC
    InputAction talkAction;

    private void Awake()
    { 
        playerInputActionMap = playerInputController.FindActionMap("PlayerMovement");
        moveAction = playerInputActionMap.FindAction("Movement");
        launchAction = playerInputActionMap.FindAction("Launch");
        talkAction = playerInputActionMap.FindAction("Talk");
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        launchAction.Enable();
        talkAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        launchAction.Disable();
        talkAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        launchAction.performed += Launch;
        launchAction.canceled += Launch;

        talkAction.performed += FindFriend;
        talkAction.canceled += FindFriend;

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

    void Launch(InputAction.CallbackContext callbackContext)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        //Debug.Log(projectile);
        projectile.Launch(moveDirection, 300);

        animator.SetTrigger("Launch");
    }

    //void FindFriend(InputAction.CallbackContext callbackContext)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, 10.0f, LayerMask.GetMask("NPC"));

    //    if(hit.collider != null)
    //    {
    //        //Debug.Log(hit.collider.name);
    //        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
    //        if (character != null)
    //        {
    //            UIHandler.instance.DisplayDialogue();
    //        }
    //    }
    //}

#if COMMENT_REGION
    void FindFriend(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.ReadValueAsButton())
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, moveDirection, 10.0f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    UIHandler.instance.DisplayDialogue();
                }
            }
        }
        



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine((Vector2)this.transform.position, Vector2.up * 10.0f);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, moveDirection, 10.0f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)this.transform.position, Vector2.up * 10);
        }


    }

#endif
}
