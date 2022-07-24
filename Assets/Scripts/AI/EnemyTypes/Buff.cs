using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[4];
    [SerializeField] SkinnedMeshRenderer myRenderer;

    protected override void StartAlt()
    {
        gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
        Material myMat = myRenderer.material;

        predictionChase = Random.Range(4, 10);
        predictionShoot = Random.Range(3, 7);

        enemyColor = (CharColours)Random.Range(0, 4);
        Debug.Log(enemyColor);
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
        moveSpeed = Random.Range(0.005f, 0.03f);
    }
}