using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected CharColours enemyColor = CharColours.Any;
    protected float distanceFromTarget;
    protected float predictionChase = 3, predictionShoot = 1.5f;
    protected float moveSpeed;
    [SerializeField]
    protected GameObject bullet;
    protected GameObject target;
    Rigidbody rb;
    
    public CharColours getColour()
    {
        return enemyColor;
    }

    protected void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
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
            Shoot();
        }
        else if (inRange(distanceFromTarget) && !inShootRange(distanceFromTarget) && !condition())
        {
            Hunt();
        }
        else if (!inRange(distanceFromTarget) && !condition())
        {
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
            predictedPosition = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, predictionChase);
            predictionTimer = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, predictedPosition, moveSpeed * 0.5f * Time.deltaTime);
    }
    protected float predictionTimer = 0;
    protected Vector3 predictPosition(Vector3 targetPosition, Vector3 targetVelocity, float waitValue)
    {
        float predictionDeviation = Random.Range(0.5f, 2f);
        Vector3 prediction = targetPosition + targetVelocity * (waitValue * predictionDeviation);
        return prediction;
    }
   

    protected void Shoot()
    {
        Vector3 prediction = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, predictionShoot);
        predictionTimer += Time.deltaTime;
        if(predictionTimer > predictionShoot)
        {
            print("hit");
            Projectile projectileClass = Instantiate(bullet, transform.position + transform.forward, transform.rotation).GetComponent<Projectile>();
            projectileClass.SetDirection(transform.forward);
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
        Destroy(gameObject);
    }

}

