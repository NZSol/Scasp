using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected CharColours enemyColor = CharColours.Blue;
    protected float distanceFromTarget;
    protected float shootMinRange, shootMaxRange;
    protected float moveSpeed = 0.5f;
    protected GameObject target, bullet, bulletSpawnPos;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
    }


    protected bool inRange(float distance)
    {
        if(distance < 5)
        {
            return true;
        }
        return false;
    }
    protected bool condition()
    {
        return false;
    }



    private void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (inRange(distanceFromTarget) && !condition())
        {
            Shoot();
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
    protected void Follow()
    {
        /*
        /. Get target location + velocity 
        /. Predict a position 
        /. travel for x seconds 
        /. repeat unless in range 
        */
        var targetBody = target.GetComponent<Rigidbody>();
        predictionTimer += Time.deltaTime;
        if (predictionTimer > 3)
        {
            Vector3 predictedPosition = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, 3);
            predictionTimer = 0;
        }
    }
    protected float predictionTimer = 0;
    protected Vector3 predictPosition(Vector3 targetPosition, Vector3 targetVelocity, float waitValue)
    {
        Vector3 prediction = targetPosition + targetVelocity * waitValue;
        return prediction;
    }
   

    protected void Shoot()
    {
        Vector3 prediction = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, 1.5f);
        if(predictionTimer < 1.5f)
        {
            Projectile projectileClass = Instantiate(bullet, bulletSpawnPos.transform.position, transform.rotation).GetComponent<Projectile>();
            projectileClass.SetDirection(transform.forward);
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

    protected void Knockback()
    {
        /*Instruction
        1. Get direction of character - target  
        2. Freeze character control 
        3. Apply force in gathered direction to character based on range from blast area
        4. Decelerate until stopped
        5. Return control to character
        */
    }

    protected void Die()
    {
        /*
        1.Play particle effect
        2. destroy character
        3. Set ground color to splatter colour
        */
    }

}

