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
    [SerializeField] GameObject explosion, bulletTrail;
    [SerializeField] ParticleSystem gunSmoke;
    [SerializeField] Transform cockpitScreenTankBaseRep;
    float leftThrottleVal, rightThrottleVal;

    int health = 5;

    #region public values
    public void setLeftTreadThrottleVal(float value)
    {
        leftThrottleVal = value;
    }

    public void setRightTreadThrottleVal(float value)
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

    public void shoot(CharColours colour)
    {
        gunSmoke.Play();
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, Mathf.Infinity))
        {
            createNewBulletLine(shootPoint.position, hit.point, true);
            Instantiate(explosion, hit.point, Quaternion.identity);
            if(hit.collider.gameObject.GetComponent<EnemyBase>() != null)
            {
                EnemyBase enemy = hit.collider.gameObject.GetComponent<EnemyBase>();
                if (enemy.getColour() == colour || enemy.getColour() == CharColours.Any) enemy.Die();
                else enemy.Knockback();
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
        else trailScript.point1Point = shootPoint.transform.forward * 1000;
    }

    public void reduceHealth()
    {
        health--;
        if(health == 0)
        {
            //PARTICLE EFFECT>????!??{!?>!?!?!??>!
            Destroy(gameObject);
            Invoke("reloadScene", 3);
            //INVOKE END GAME!!!!!!
        }
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
