using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : EnemyBase
{
    private void Start()
    {
        moveSpeed = 0.025f;
        enemyColor = CharColours.Any; 
    }
}
