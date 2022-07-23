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

    // Update is called once per frame
    void Update()
    {
        
    }
}
