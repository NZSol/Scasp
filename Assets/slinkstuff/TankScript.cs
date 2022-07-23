using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //only need this for test UI sliders

public class TankScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] Transform turretTransform;

    //these are our values for tread-based acceleration
    float rightTreadAccelValue, leftTreadAccelValue;
    [SerializeField] float topSpeed, accelMultiplier, rotationMultiplier;

    float turretCurrentRotation, turretRotationChangeVal;
    [SerializeField] float turretRotateSpeed , turretRotateLerpSpeed;

    //test only stuff
    [SerializeField] Slider leftThrottle, rightThrottle, rotationThrottle;

    float leftThrottleVal, rightThrottleVal;

    #region public values
    public void setLeftTreadThrottleVal(float value)
    {
        leftThrottleVal = value;
    }

    public void setRightThrottleVal(float value)
    {
        rightThrottleVal = value;
    }

    public void setTurretTurnValue(float value)
    {
        turretRotationChangeVal = value;
    }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        setLeftTreadThrottleVal(leftThrottle.value);
        setRightThrottleVal(rightThrottle.value);
        setTurretTurnValue(rotationThrottle.value);
        //end testing

        //lerp up seperate throttles, don't need to lerp full accel that way
        rightTreadAccelValue = Mathf.Lerp(rightTreadAccelValue, rightThrottleVal, Time.fixedDeltaTime * accelMultiplier);
        leftTreadAccelValue = Mathf.Lerp(leftTreadAccelValue, leftThrottleVal, Time.fixedDeltaTime * accelMultiplier);

        //tank rotation!!
        //transform.Translate(new Vector3(0, 0, (leftTreadAccelValue + rightTreadAccelValue) * Time.deltaTime));
        transform.RotateAround(transform.position, Vector3.up, (leftTreadAccelValue + (rightTreadAccelValue * -1)) * Time.deltaTime * rotationMultiplier);

        //turret rotation
        turretCurrentRotation = Mathf.Lerp(turretCurrentRotation, turretRotationChangeVal, Time.deltaTime * turretRotateLerpSpeed);
        turretTransform.RotateAround(turretTransform.position, turretTransform.up, turretCurrentRotation * turretRotateSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < topSpeed) rb.velocity = transform.forward * (leftTreadAccelValue + rightTreadAccelValue);
    }
}
