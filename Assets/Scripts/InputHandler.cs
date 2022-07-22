using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private TempCharacterController myMainScript;
    private bool interacting = false;

    void Start()
    {
        myMainScript = gameObject.GetComponent<TempCharacterController>();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            myMainScript.interacting = true;
        }
        else if (context.canceled)
        {
            myMainScript.interacting = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveVals = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            moveVals = Vector2.zero;
        }
        myMainScript.moveVector = moveVals;
    }
}
