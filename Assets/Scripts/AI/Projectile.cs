using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction = new Vector3();
    
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        gameObject.GetComponent<Rigidbody>().AddForce(direction * 5, ForceMode.Impulse);
    }
    float timeToDie = 5;

    // Update is called once per frame
    void Update()
    {
        timeToDie -= Time.deltaTime;
        if(timeToDie < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            collision.gameObject.GetComponent<TankScript>().reduceHealth();
            Destroy(gameObject);
        }

    }
}
