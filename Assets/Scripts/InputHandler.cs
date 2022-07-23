using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private TempCharacterController myMainScript;

    void Start()
    {
        myMainScript = gameObject.GetComponent<TempCharacterController>();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            myMainScript.interacting = true;
            StartCoroutine(interactingBuffer());
        }
    }

    IEnumerator interactingBuffer()
    {
        yield return new WaitForSeconds(0.3f);
        myMainScript.interacting = false;
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
