using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[4];

    protected override void StartAlt()
    {
        predictionChase = Random.Range(4, 10);
        predictionShoot = Random.Range(3, 7);
        do
        {
            enemyColor = (CharColours)Random.Range(0, 4);
        } while ((int)enemyColor < 0 || (int)enemyColor > 3);
        gameObject.GetComponent<Renderer>().material = mats[(int)enemyColor];
        moveSpeed = Random.Range(0.005f, 0.03f);
    }
}
