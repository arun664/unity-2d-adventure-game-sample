using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] InputActionAsset playerInputController;
    InputActionMap playerInputActionMap;
    InputAction moveAction;
    Vector2 axis = default(Vector2);

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
        moveAction.performed += Move;
        moveAction.canceled += Move;

    }
    // Update is called once per frame
    void Update()
    {
        UpdateMovement(axis);
    }



    void Move(InputAction.CallbackContext ctx)
    {
        axis = ctx.ReadValue<Vector2>();
    }

    void UpdateMovement(Vector2 axis)
    {
        Vector2 movementInput = axis;

        //legacy normalization if mode --DIGITAL NORMALIZE doesn't exist in ActionMap
        if(movementInput.magnitude > 1) { 
            movementInput.Normalize();
        }

        print(movementInput.magnitude);
       // print(movementInput);
        Vector2 position = (Vector2)transform.position + 3.0f * Time.deltaTime * movementInput;
        transform.position = position;
    }


    void PrintLog(string msg)
    {
        Debug.Log(msg);
    }
}
