using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTargetScript : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) transform.position = target.transform.position;
    }
}
