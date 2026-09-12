using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum zoneKind
    {
        NULL,
        TREADRIGHT,
        TREADLEFT,
        AIMING,
        AMMO,
        BARREL,
        TREADBOTH, //For low player count
        FIRE//For low player count
    }
    public zoneKind myZone;
    public bool occupied = false;
}
