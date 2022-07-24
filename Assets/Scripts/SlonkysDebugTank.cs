using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlonkysDebugTank : MonoBehaviour
{
    TankScript script;
    [Range(-5,5)]
    [SerializeField] float left = 0, right = 0;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<TankScript>();
        print(240 % 60/9);
    }

    // Update is called once per frame
    void Update()
    {
        script.setLeftTreadThrottleVal(left);
        script.setRightTreadThrottleVal(right);
    }
}
