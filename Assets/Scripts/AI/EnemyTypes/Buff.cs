using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[4];

    void Start()
    {
        do
        {
            enemyColor = (CharColours)Random.Range(0, 4);
        } while ((int)enemyColor < 0 || (int)enemyColor > 3);
        gameObject.GetComponent<Renderer>().material = mats[(int)enemyColor];
        moveSpeed = 0.01f;

    }
    
}
