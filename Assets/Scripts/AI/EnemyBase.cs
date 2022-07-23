using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected CharColours enemyColor = CharColours.Any;
    protected float distanceFromTarget;
    protected float predictionChase = 10, predictionShoot = 1.5f;
    protected float moveSpeed;
    [SerializeField]
    protected GameObject target, bullet, bulletSpawnPos;
    [SerializeField] GameObject obj;
    
    public CharColours getColour()
    {
        return enemyColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
        predictionChase = Random.Range(4, 10);
    }


    protected bool inRange(float distance)
    {
        if(distance < 15)
        {
            return true;
        }
        return false;
    }
    protected bool condition()
    {
        return false;
    }



    protected void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (inRange(distanceFromTarget) && !condition())
        {
            Shoot();
            print("Shooting");
            predictionTimer = predictionChase;
        }
        else if (!inRange(distanceFromTarget) && !condition())
        {
            Follow();
            print("Moving to position");
        }
    }

    /*Priority list
    1. Shoot
    2. Follow

      Conditional List
    1. Knockback
    2. Die
    */
    Vector3 predictedPosition = new Vector3();
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
        if (predictionTimer > predictionChase)
        {
            predictedPosition = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, predictionChase);
            predictionTimer = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, predictedPosition, 0.025f);
    }
    protected float predictionTimer = 0;
    protected Vector3 predictPosition(Vector3 targetPosition, Vector3 targetVelocity, float waitValue)
    {
        Vector3 prediction = targetPosition + targetVelocity * waitValue;
        Instantiate(obj, prediction, transform.rotation);
        return prediction;
    }
   

    protected void Shoot()
    {
        Vector3 prediction = predictPosition(target.transform.position, target.GetComponent<Rigidbody>().velocity, predictionShoot);
        if(predictionTimer < predictionShoot)
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

    public void Knockback()
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
        /*
        1.Play particle effect
        2. destroy character
        3. Set ground color to splatter colour
        */
    }

}

