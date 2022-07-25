using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[4];
    [SerializeField] SkinnedMeshRenderer myRenderer;
    [SerializeField] SpriteRenderer circleSprite;
    [SerializeField] float myMoveSpeed, minChasePredictionTime, maxChasePredictionTime, minShootPredictionTime, maxShootPredictionTime;
    MultiplayerHandler mpHandler;

    protected override void StartAlt()
    {
        mpHandler = GameObject.Find("God").GetComponent<MultiplayerHandler>();
        gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
        Material myMat = myRenderer.material;

        predictionChase = Random.Range(minChasePredictionTime, maxChasePredictionTime);
        predictionShoot = Random.Range(minShootPredictionTime, maxShootPredictionTime);

        enemyColor = (CharColours)Random.Range(0, mpHandler.Players.Count);
        //Debug.Log(enemyColor);
        circleSprite.color = GameObject.Find("God").GetComponent<MultiplayerHandler>().playerUIColours[(int)enemyColor];
        switch (enemyColor)
        {
            case CharColours.Blue:
                myMat = mats[0];
                break;
            case CharColours.Red:
                myMat = mats[1];
                break;
            case CharColours.Orange:
                myMat = mats[2];
                break;
            case CharColours.Green:
                myMat = mats[3];
                break;
        }
        myRenderer.material = myMat;

        gameObject.GetComponent<Renderer>().material = mats[(int)enemyColor];
        moveSpeed = myMoveSpeed;
    }
}