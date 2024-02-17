using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] InputActionAsset playerInputController;
    InputActionMap playerInputActionMap;
    InputAction moveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;

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

    }
    // Update is called once per frame
    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        Debug.Log(move);
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void PrintLog(string msg)
    {
        Debug.Log(msg);
    }
}
