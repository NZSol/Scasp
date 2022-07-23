using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempCharacterController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float movespeedCap = 5f;
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float deccelerationMultiplier = 0.8f;
    [SerializeField] private float shellMovementImpairment = 0.5f;
    [SerializeField] private float shellLoadTime = 0.2f;
    [SerializeField] private TankScript theTank = null;
    [SerializeField] private MeshRenderer myRenderer;
    public int playerNum = 0;

    public Vector2 moveVector = Vector2.zero;
    public bool interacting = false;
    private Zone targetZone = null;

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
    private CharColours myColour = CharColours.Red;
    [SerializeField] playerState currentState = playerState.IDLE;
    [SerializeField] Zone.zoneKind currentZone = Zone.zoneKind.NULL;
    void Awake()
    {
        var God = GameObject.Find("God").GetComponent<MultiplayerHandler>();
        playerNum = GetComponent<PlayerInput>().user.index;
        God.Players.Add(gameObject);
        transform.position = God.spawns[playerNum].position;
        myRenderer.material = God.playerColours[playerNum];
        playerNum = God.Players.Count;
        myColour = (CharColours)playerNum;
        theTank = GameObject.Find("Tank").GetComponent<TankScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        currentState = playerState.IDLE;
    }

    #region General functions and initialization of state functions

    //Runs Transitions to zone controls
    void OnTriggerStay(Collider other)
    {
        if (interacting)
        {
            if (other.tag == "ActionZone" && other.GetComponent<Zone>().occupied == false)
            {
                switch (other.GetComponent<Zone>().myZone)
                {
                    case Zone.zoneKind.AIMING:
                        if (currentState != playerState.IDLESHELL && currentState != playerState.MOVESHELL)
                        {
                            currentZone = Zone.zoneKind.AIMING;
                            other.GetComponent<Zone>().occupied = true;
                            targetZone = other.GetComponent<Zone>();
                            currentState = playerState.MODULECONTROL;
                        }
                        break;
                    case Zone.zoneKind.AMMO:
                        if (currentState != playerState.IDLESHELL && currentState != playerState.MOVESHELL)
                        {
                            currentZone = Zone.zoneKind.AMMO;
                            targetZone = other.GetComponent<Zone>();
                            currentState = playerState.IDLESHELL;
                        }
                        break;
                    case Zone.zoneKind.BARREL:
                        if(currentState == playerState.IDLESHELL || currentState == playerState.MOVESHELL)
                        {
                            currentZone = Zone.zoneKind.BARREL;
                            other.GetComponent<Zone>().occupied = true;
                            targetZone = other.GetComponent<Zone>();
                            currentState = playerState.MODULECONTROL;
                        }
                        break;
                    case Zone.zoneKind.TREADLEFT:
                        if (currentState != playerState.IDLESHELL && currentState != playerState.MOVESHELL)
                        {
                            currentZone = Zone.zoneKind.TREADLEFT;
                            other.GetComponent<Zone>().occupied = true;
                            targetZone = other.GetComponent<Zone>();
                            currentState = playerState.MODULECONTROL;
                        }
                        break;
                    case Zone.zoneKind.TREADRIGHT:
                        if (currentState != playerState.IDLESHELL && currentState != playerState.MOVESHELL)
                        {
                            currentZone = Zone.zoneKind.TREADRIGHT;
                            other.GetComponent<Zone>().occupied = true;
                            targetZone = other.GetComponent<Zone>();
                            currentState = playerState.MODULECONTROL;
                        }
                        break;
                }
            }
        }
    }

    void Update()
    {
        if(moveVector != Vector2.zero)
        {
            Vector3 moveDir = (moveVector.x * Vector3.right) + (moveVector.y * Vector3.forward);
            transform.GetChild(0).transform.rotation = Quaternion.Slerp(myRenderer.transform.rotation, Quaternion.LookRotation(moveDir), 0.25f);
        }
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
        rb.velocity = rb.velocity * deccelerationMultiplier;
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
        rb.velocity = rb.velocity * 0.2f;
        //Acceleration:
        if (rb.velocity.magnitude < movespeedCap * moveVector.magnitude)
        {
            rb.velocity += (new Vector3(moveVector.x * acceleration, 0, moveVector.y * acceleration));
        }
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
        /*Change to MOVESHELL*/
        if (moveVector != Vector2.zero)
        {
            currentState = playerState.MOVESHELL;
        }
    }
    private void IDLESHELLFixedUpdate()
    {
        rb.velocity = rb.velocity * (deccelerationMultiplier);
    }
    #endregion
    #region MOVESHELL Functions
    private void MOVESHELLUpdate()
    {
        /*Change to IDLESHELL*/
        if (moveVector == Vector2.zero)
        {
            currentState = playerState.IDLESHELL;
        }
    }
    private void MOVESHELLFixedUpdate()
    {
        rb.velocity = rb.velocity * 0.2f;
        //Acceleration:
        if (rb.velocity.magnitude < (movespeedCap * shellMovementImpairment) * moveVector.magnitude)
        {
            rb.velocity += (new Vector3(moveVector.x * (acceleration * shellMovementImpairment), 0, moveVector.y * (acceleration * shellMovementImpairment)));
        }
    }
    #endregion
    #region MODULECONTROL Functions
    private void MODULECONTROLUpdate()
    {
        rb.velocity = rb.velocity * deccelerationMultiplier;
        if (!interacting)
        {
            targetZone.occupied = false;
            currentZone = Zone.zoneKind.NULL;
            currentState = playerState.IDLE;
        }
        switch (currentZone)
        {
            case Zone.zoneKind.AIMING:
                float aimOutput = moveVector.x;
                theTank.setTurretTurnValue(aimOutput * 5);
                break;
            case Zone.zoneKind.AMMO:
                break;
            case Zone.zoneKind.BARREL:
                StartCoroutine(waitForSeconds(shellLoadTime));
                break;
            case Zone.zoneKind.TREADLEFT:
                float leftOutput = moveVector.y;
                theTank.setLeftTreadThrottleVal(leftOutput * 5);
                break;
            case Zone.zoneKind.TREADRIGHT:
                float rightOutput = moveVector.y;
                theTank.setRightTreadThrottleVal(rightOutput * 5);
                break;
        }
    }
    private void MODULECONTROLFixedUpdate()
    {

    }

    IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        theTank.shoot(myColour);
        targetZone.occupied = false;
        currentZone = Zone.zoneKind.NULL;
        currentState = playerState.IDLE;
    }
    #endregion
}
