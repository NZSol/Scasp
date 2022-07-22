using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBuddy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, lerpSpeed * Time.deltaTime);
    }
}
