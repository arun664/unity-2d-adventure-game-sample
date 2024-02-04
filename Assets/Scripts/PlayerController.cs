using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction leftAction;

    // Start is called before the first frame update
    void Start()
    {
        leftAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.01f;
        float vertical = 0.01f;

        if (leftAction.IsPressed())
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {

            horizontal = 1.0f;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
        }

        //Debug.Log("horizontal: "+horizontal +"vertical: " +vertical);

        Vector2 position = transform.position;

        position.x += 0.01f * horizontal;
        position.y += 0.01f * vertical;

        //Debug.Log("position x: " + position.x + "position y: " + position.y);
        transform.position = position;
    }
}
