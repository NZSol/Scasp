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
        BARREL
    }
    public zoneKind myZone;
    public bool occupied = false;
}
