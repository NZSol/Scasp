using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //only need this for test UI sliders

public class TankScript : MonoBehaviour
{
    [SerializeField] Transform turretTransform;

    //these are our values for tread-based acceleration
    float rightTreadAccelValue, leftTreadAccelValue;
    [SerializeField] float accelMultiplier, rotationMultiplier;

    float turretRotationValue;
    [SerializeField] float turretRotateSpeed , turretRotateLerpSpeed;


    //test only stuff
    [SerializeField] Slider leftThrottle, rightThrottle, rotationThrottle;

    // Update is called once per frame
    void Update()
    {
        //lerp up seperate throttles, don't need to lerp full accel that way
        rightTreadAccelValue = Mathf.Lerp(rightTreadAccelValue, rightThrottle.value, Time.deltaTime * accelMultiplier);
        leftTreadAccelValue = Mathf.Lerp(leftTreadAccelValue, leftThrottle.value, Time.deltaTime * accelMultiplier);

        transform.Translate(new Vector3(0, 0, (rightTreadAccelValue + leftTreadAccelValue) * Time.deltaTime));
        transform.RotateAround(transform.position, Vector3.up, (leftTreadAccelValue + (rightTreadAccelValue * -1)) * Time.deltaTime * rotationMultiplier);

        //turret rotation
        turretRotationValue = Mathf.Lerp(turretRotationValue, rotationThrottle.value, Time.deltaTime * turretRotateLerpSpeed);
        turretTransform.RotateAround(turretTransform.position, turretTransform.up, turretRotationValue * turretRotateSpeed * Time.deltaTime);

    }
}
