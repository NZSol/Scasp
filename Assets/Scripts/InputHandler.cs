using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private TempCharacterController myMainScript;

    void Awake()
    {
        myMainScript = gameObject.GetComponent<TempCharacterController>();
    }

    public void startGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var god = GameObject.Find("God");
            if (!god.GetComponent<MultiplayerHandler>().gameStarted)
            {
                god.GetComponent<RoundHandler>().startGame();
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            myMainScript.interacting = true;
        }
        if (context.canceled)
        {
            myMainScript.interacting = false;
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
