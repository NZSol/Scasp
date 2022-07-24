using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : EnemyBase
{
    protected override void StartAlt()
    {
        moveSpeed = Random.Range(0.025f, 0.04f);
        predictionChase = Random.Range(2, 6);
        predictionShoot = Random.Range(1, 5);
        enemyColor = CharColours.Any;
    }
}
