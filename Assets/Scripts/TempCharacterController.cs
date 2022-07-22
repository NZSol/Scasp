using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharacterController : MonoBehaviour
{
    //- No proper state machine because of time constraints, setting up a local Enum to function as if there is one -\\
    enum playerState
    {
        IDLE,
        MOVE,
        BOUNCED,
        IDLESHELL,
        MOVESHELL,
        TREADCONTROL
    }
    [SerializeField] playerState currentState = playerState.IDLE;
    void Start()
    {

    }

    #region General functions and initialization of state functions
    void Update()
    {
        //Initializing update functions of states
        switch (currentState)
        {
            case playerState.IDLE:
                break;
            case playerState.MOVE:
                break;
            case playerState.BOUNCED:
                break;
            case playerState.IDLESHELL:
                break;
            case playerState.MOVESHELL:
                break;
            case playerState.TREADCONTROL:
                break;
        }
    }

    void FixedUpdate()
    {
        //Initializing fixedupdate functions of states
        switch (currentState)
        {
            case playerState.IDLE:
                break;
            case playerState.MOVE:
                break;
            case playerState.BOUNCED:
                break;
            case playerState.IDLESHELL:
                break;
            case playerState.MOVESHELL:
                break;
            case playerState.TREADCONTROL:
                break;
        }
    }
    #endregion

    #region IDLE Functions
    private void IDLEUpdate()
    {

    }
    private void IDLEFixedUpdate()
    {

    }
    #endregion
    #region MOVE Functions
    private void MOVEUpdate()
    {

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
    private void TREADCONTROLUpdate()
    {

    }
    private void TREADCONTROLFixedUpdate()
    {

    }
    #endregion
}
