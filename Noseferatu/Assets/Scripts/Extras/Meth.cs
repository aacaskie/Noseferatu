using UnityEngine;
using System.Collections;

/// <summary>
/// Math extension functions
/// Extra number functions for fun and profit
/// </summary>
public static class Meth {

    public static float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }

}
