using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 GetXz(this Vector3 value)
    {
        value.y = 0;

        return value;
    }
}
