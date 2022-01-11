using UnityEngine;
using System.Collections;

public class Util {
    public static float DistanceBetween(Character t1, Character t2) 
    {
        var dx = t2.X - t1.X;
        var dy = t2.Y - t1.Y;
        return Mathf.Sqrt(dx*dx + dy*dy);
    }
}
