using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected CharColours enemyColor = CharColours.Any;
    protected float distanceFromTarget;
    protected float predictionChase = 3, predictionShoot = 3f;
    protected float moveSpeed;
    [SerializeField]
    protected GameObject bullet;
    protected GameObject target;
    Rigidbody rb;
    [SerializeField] protected GameObject shootPredictionHelperObject, otherHelper;
    protected AudioSource enemyAudio;
    [SerializeField] protected AudioClip shootSound, dieSound;
    [SerializeField] protected GameObject deathFX;
    public CharColours getColour()
    {
        return enemyColor;
    }

    protected void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
        enemyAudio = GameObject.Find("Enemy Audio Source").GetComponent<AudioSource>();
        StartAlt();
    }

    protected abstract void StartAlt();



    protected bool inRange(float distance)
    {
        if(distance < 50)
        {
            return true;
        }
        return false;
    }
    protected bool condition()
    {
        return false;
    }

    protected bool inShootRange(float distance)
    {
        if (distance < 15)
            return true;
        else
            return false;
    }


    protected void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (inRange(distanceFromTarget) && inShootRange(distanceFromTarget) && !condition())
        {
            transform.LookAt(target.transform.position);
            Shoot();
        }
        else if (inRange(distanceFromTarget) && !inShootRange(distanceFromTarget) && !condition())
        {
            //print("Hunting");
            Hunt();
        }
        else if (!inRange(distanceFromTarget) && !condition())
        {
            //print("Following");
            Follow();
        }
    }

    /*Priority list
    1. Shoot
    2. Follow

      Conditional List
    1. Knockback
    2. Die
    */
    protected void Hunt()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);
    }

    Vector3 predictedPosition = new Vector3();
    protected void Follow()
    {
        predictionTimer += Time.deltaTime;
        if (predictionTimer > predictionChase)
        {
            predictedPosition = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, predictionChase, false);
            predictionTimer = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, predictedPosition, moveSpeed * Time.deltaTime);
        transform.LookAt(predictedPosition);
    }

    protected float predictionTimer = 0;

    protected Vector3 predictPosition(Vector3 targetPosition, Vector3 targetVelocity, float waitValue, bool shooting)
    {
        RaycastHit hit;
        float predictionDeviation = Random.Range(0.25f, 1.5f);
        Vector3 prediction = targetPosition + targetVelocity * (waitValue * predictionDeviation);
        if(!shooting && Physics.Raycast(transform.position, prediction - transform.position * 25, out hit))
        {
            if (hit.transform.name.Contains("Wall"))
            {
                prediction += transform.right * 25;
            }
        }
        return prediction;
    }
   

    protected void Shoot()
    {
        Vector3 prediction = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, 0, true);
        predictionTimer += Time.deltaTime;
        if (predictionTimer > predictionShoot)
        {
            Vector3 fireLine = prediction - transform.position;
            transform.LookAt(prediction);
            Debug.Log("Shooting at " + fireLine);
            Projectile projectileClass = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z) + transform.forward, Quaternion.identity).GetComponent<Projectile>();
            projectileClass.SetDirection(fireLine.normalized);
            enemyAudio.PlayOneShot(shootSound, Random.Range(.005f, .01f));
            predictionTimer -= predictionTimer;     
        }

        /*Instructions{
        /. Get target location 
        /. Get target momentum velocity 
        /. Calculate position from velocity and postition  
        /. Fire weapon at predicted location 
            /5a. Projectile travels in line 
            5b. Projectile tracks player lightly
        */


    }

    public void Knockback(Vector3 point)
    {
        /*Instruction
        1. Get direction of character - target  
        2. Freeze character control 
        3. Apply force in gathered direction to character based on range from blast area
        4. Decelerate until stopped
        5. Return control to character
        */

    }

    public void Die()
    {
        //INVOKME DIEiEIE???!!?!!??!?!?!?!?!?!?
        enemyAudio.PlayOneShot(dieSound, 0.5f);
        if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

