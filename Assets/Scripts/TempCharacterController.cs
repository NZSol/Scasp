using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharacterController : MonoBehaviour
{
    public Vector2 moveVector = Vector2.zero;
    public bool interacting = false;

    //- No proper state machine because of time constraints, setting up a local Enum to function as if there is one -\\
    enum playerState
    {
        IDLE,
        MOVE,
        BOUNCED,
        IDLESHELL,
        MOVESHELL,
        MODULECONTROL
    }
    [SerializeField] playerState currentState = playerState.IDLE;
    void Start()
    {
        currentState = playerState.IDLE;
    }

    #region General functions and initialization of state functions
    void Update()
    {
        //Initializing update functions of states
        switch (currentState)
        {
            case playerState.IDLE:
                IDLEUpdate();
                break;
            case playerState.MOVE:
                MOVEUpdate();
                break;
            case playerState.BOUNCED:
                BOUNCEDUpdate();
                break;
            case playerState.IDLESHELL:
                IDLESHELLUpdate();
                break;
            case playerState.MOVESHELL:
                MOVESHELLUpdate();
                break;
            case playerState.MODULECONTROL:
                MODULECONTROLUpdate();
                break;
        }
    }

    void FixedUpdate()
    {
        //Initializing fixedupdate functions of states
        switch (currentState)
        {
            case playerState.IDLE:
                IDLEFixedUpdate();
                break;
            case playerState.MOVE:
                MOVEFixedUpdate();
                break;
            case playerState.BOUNCED:
                BOUNCEDFixedUpdate();
                break;
            case playerState.IDLESHELL:
                IDLESHELLFixedUpdate();
                break;
            case playerState.MOVESHELL:
                MOVESHELLFixedUpdate();
                break;
            case playerState.MODULECONTROL:
                MODULECONTROLFixedUpdate();
                break;
        }
    }
    #endregion

    #region IDLE Functions
    private void IDLEUpdate()
    {
        /*Change to MOVE*/ if (moveVector != Vector2.zero)
        {
            currentState = playerState.MOVE;
        }
    }
    private void IDLEFixedUpdate()
    {

    }
    #endregion
    #region MOVE Functions
    private void MOVEUpdate()
    {
        /*Change to IDLE*/ if (moveVector == Vector2.zero)
        {
            currentState = playerState.IDLE;
        }
    }
    private void MOVEFixedUpdate()
    {

    }
    #endregion
    #region BOUNCED Functions
    private void BOUNCEDUpdate()
    {

    }
    private void BOUNCEDFixedUpdate()
    {

    }
    #endregion
    #region IDLESHELL Functions
    private void IDLESHELLUpdate()
    {

    }
    private void IDLESHELLFixedUpdate()
    {

    }
    #endregion
    #region MOVESHELL Functions
    private void MOVESHELLUpdate()
    {

    }
    private void MOVESHELLFixedUpdate()
    {

    }
    #endregion
    #region TREADCONTROL Functions
    private void MODULECONTROLUpdate()
    {

    }
    private void MODULECONTROLFixedUpdate()
    {

    }
    #endregion
}
