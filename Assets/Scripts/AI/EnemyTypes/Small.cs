using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : EnemyBase
{
    [SerializeField] float myMoveSpeed, minChasePredictionTime, maxChasePredictionTime, minShootPredictionTime, maxShootPredictionTime;
    protected override void StartAlt()
    {
        moveSpeed = myMoveSpeed;
        predictionChase = Random.Range(minChasePredictionTime, maxChasePredictionTime);
        predictionShoot = Random.Range(minShootPredictionTime, maxShootPredictionTime);
        enemyColor = CharColours.Any;
    }
}
