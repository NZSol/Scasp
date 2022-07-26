using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //only need this for test UI sliders

public class TankScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] Transform turretTransform, shootPoint;

    //these are our values for tread-based acceleration
    float rightTreadAccelValue, leftTreadAccelValue;
    [SerializeField] float topSpeed, accelMultiplier, rotationMultiplier;

    float turretCurrentRotation, turretRotationChangeVal;
    [SerializeField] float turretRotateSpeed , turretRotateLerpSpeed;
    [SerializeField] GameObject explosion, bigExplosion, bulletTrail;
    [SerializeField] ParticleSystem gunSmoke;
    [SerializeField] Transform cockpitScreenTankBaseRep;
    float leftThrottleVal, rightThrottleVal;
    spinnyLightsScript lights;
    RoundHandler rHandler;
    [SerializeField] GameObject youDiedUI;
    [SerializeField] LayerMask stuffShotsDontGoThrough;
    [SerializeField] Slider healthSlider, LThrottleSlider, RThrottleSlider;
    [SerializeField] AudioSource aud, engineAudio;
    [SerializeField] AudioClip tankShotSound;


    bool leftThrottleReceivedInput, rightThrottleReceivedInput, turretReceivedInput;

    int health = 5;

    #region public values
    public void setLeftTreadThrottleVal(float value)
    {
        leftThrottleReceivedInput = true;
        leftThrottleVal = value;
    }

    public void setRightTreadThrottleVal(float value)
    {
        rightThrottleReceivedInput = true;
        rightThrottleVal = value;
    }

    public void setTurretTurnValue(float value)
    {
        turretReceivedInput = true;
        turretRotationChangeVal = value;
    }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var god = GameObject.Find("God");
        rHandler = god.GetComponent<RoundHandler>();
    }

    public void shoot(CharColours colour)
    {
        gunSmoke.Play();
        aud.PlayOneShot(tankShotSound);
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, Mathf.Infinity, stuffShotsDontGoThrough))
        {
            createNewBulletLine(shootPoint.position, hit.point, true);
            Instantiate(explosion, hit.point, Quaternion.identity);
            if(hit.collider.gameObject.GetComponent<EnemyBase>() != null)
            {
                EnemyBase enemy = hit.collider.gameObject.GetComponent<EnemyBase>();
                if (enemy.getColour() == colour || enemy.getColour() == CharColours.Any) enemy.Die();
                else enemy.Knockback(hit.point);
            }
        }
        else
        {
            Debug.Log("HA!!!");
            createNewBulletLine(shootPoint.position, hit.point, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //lerp up seperate throttles, don't need to lerp full accel that way
        rightTreadAccelValue = Mathf.Lerp(rightTreadAccelValue, rightThrottleVal, Time.fixedDeltaTime * accelMultiplier);
        leftTreadAccelValue = Mathf.Lerp(leftTreadAccelValue, leftThrottleVal, Time.fixedDeltaTime * accelMultiplier);

        //tank rotation!!
        //transform.Translate(new Vector3(0, 0, (leftTreadAccelValue + rightTreadAccelValue) * Time.deltaTime));
        transform.RotateAround(transform.position, Vector3.up, (leftTreadAccelValue + (rightTreadAccelValue * -1)) * Time.deltaTime * rotationMultiplier);

        //turret rotation
        turretCurrentRotation = Mathf.Lerp(turretCurrentRotation, turretRotationChangeVal, Time.deltaTime * turretRotateLerpSpeed);
        turretTransform.RotateAround(turretTransform.position, turretTransform.up, turretCurrentRotation * turretRotateSpeed * Time.deltaTime);
        cockpitScreenTankBaseRep.RotateAround(cockpitScreenTankBaseRep.position, Vector3.up, (turretCurrentRotation * -1) * turretRotateSpeed * Time.deltaTime);

        calculateEngineAudioPan();

        LThrottleSlider.value = leftTreadAccelValue;
        RThrottleSlider.value = rightTreadAccelValue;

        if (!leftThrottleReceivedInput) leftThrottleVal = 0;
        if (!rightThrottleReceivedInput) rightThrottleVal = 0;
        if (!turretReceivedInput) turretRotationChangeVal = 0;
        leftThrottleReceivedInput = false;
        rightThrottleReceivedInput = false;
        turretReceivedInput = false;

    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < topSpeed) rb.velocity = transform.forward * (leftTreadAccelValue + rightTreadAccelValue);
    }

    void createNewBulletLine(Vector3 startPoint, Vector3 endPoint, bool hitSomething)
    {
        GameObject bulletLine = Instantiate(bulletTrail, Vector3.zero, new Quaternion(0, 0, 0, 0));
        BulletLineScript trailScript = bulletLine.GetComponent<BulletLineScript>();
        trailScript.point0Point = startPoint;
        if (hitSomething) trailScript.point1Point = endPoint;
        else trailScript.point1Point = new Vector3(shootPoint.transform.forward.x, shootPoint.transform.forward.y, shootPoint.transform.forward.z) * 1000;
        
    }

    public void reduceHealth()
    {
        health--;
        healthSlider.value = health;
        if (health == 0)
        {
            GameObject.Find("User Interface/TimerBG").SetActive(false);
            Instantiate(bigExplosion, transform.position, Quaternion.identity);
            youDiedUI.SetActive(true);
            rHandler.Invoke("reloadScene", 3);
            Destroy(gameObject);
        }
    }

    void calculateEngineAudioPan()
    {
        float LMagnitude, RMagnitude;
        //get magnitudes of both throttles (so if -1 the value is still 1)
        if (leftTreadAccelValue < 0) LMagnitude = leftTreadAccelValue * -1;
        else LMagnitude = leftTreadAccelValue;

        if (rightTreadAccelValue < 0) RMagnitude = rightTreadAccelValue * -1;
        else RMagnitude = rightTreadAccelValue;

        engineAudio.panStereo = (RMagnitude - LMagnitude) / 5 * 0.5f;
        engineAudio.pitch = 1 + (RMagnitude + LMagnitude) / 5 * 0.5f;
    }
}
