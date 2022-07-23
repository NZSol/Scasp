using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[4];

    void Start()
    {
        enemyColor = (CharColours)Random.Range(0, 5);
        moveSpeed = 0.01f;
        gameObject.GetComponent<Renderer>().material = mats[(int)enemyColor];

        
    }
    
}
