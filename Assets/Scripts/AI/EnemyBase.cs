using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected CharColours enemyColor = CharColours.Blue;
    protected float distanceFromTarget;
    protected float shootMinRange, shootMaxRange;
    protected float moveSpeed = 0.5f;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
    }

    private void Update()
    {
        Follow();
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
        1. Get target location + velocity
        2. Predict a position
        3. travel for x seconds
        4. repeat unless in range
        */
        var targetBody = target.GetComponent<Rigidbody>();
        Vector3 targetPosition = PredictPosition(targetBody, target.transform.position);
        Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
    }
    protected float predictionTimer = 0;
        protected Vector3 PredictPosition(Rigidbody targetBody, Vector3 targetPos)
        {
            Vector3 prediction = new Vector3();
            predictionTimer += Time.deltaTime;
            if(predictionTimer > 3)
            {
                predictionTimer = 0;
                prediction = targetPos * targetBody.velocity.z;
                Instantiate(new GameObject("temp"), prediction, Quaternion.Euler(Vector3.zero));
            }
            prediction = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z * targetBody.velocity.z);
            return prediction;
        }

    protected void Shoot()
    {
        /*Instructions{
        1. Get target location
        2. Get target momentum velocity
        3. Calculate position from velocity and postition
        4. Fire weapon at predicted location
            5a. Projectile travels in line
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

