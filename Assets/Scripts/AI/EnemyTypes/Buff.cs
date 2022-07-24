using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EnemyBase
{
    [SerializeField]
    Material[] mats = new Material[2];

    protected override void StartAlt()
    {
        gameObject.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
        Material myMat = GetComponent<Renderer>().material;

        predictionChase = Random.Range(4, 10);
        predictionShoot = Random.Range(3, 7);

        enemyColor = (CharColours)Random.Range(0, 4);
        switch (enemyColor)
        {
            case CharColours.Blue:
                myMat.SetColor("_EmissionColor", Color.blue * 5);
                break;
            case CharColours.Red:
                myMat.SetColor("_EmissionColor", Color.red * 5);
                break;
            case CharColours.Green:
                myMat.SetColor("_EmissionColor", Color.green * 5);
                break;
            case CharColours.Orange:
                myMat.SetColor("_EmissionColor", new Color(255, 90, 0) * 5);
                break;
        }

        gameObject.GetComponent<Renderer>().material = mats[(int)enemyColor];
        moveSpeed = Random.Range(0.005f, 0.03f);
    }
}