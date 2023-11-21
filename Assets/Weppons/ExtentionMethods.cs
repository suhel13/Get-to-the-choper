using UnityEngine;
using System.Collections;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static Vector2 RotateVector(this Vector2 v2, float angle)
    {
        float angleRad = angle / Mathf.Rad2Deg;
        Vector2 temp = v2;
        v2.y = Mathf.Cos(angleRad) * temp.y - Mathf.Sin(angleRad) * temp.x;
        v2.x = Mathf.Sin(angleRad) * temp.y + Mathf.Cos(angleRad) * temp.x;
        return v2;
    }
    public static Vector3 RotateVectorByAxisZ(this Vector3 v3, float angle)
    {
        float angleRad = angle / Mathf.Rad2Deg;
        Vector3 temp = v3;
        v3.y = Mathf.Cos(angleRad) * temp.y - Mathf.Sin(angleRad) * temp.x;
        v3.x = Mathf.Sin(angleRad) * temp.y + Mathf.Cos(angleRad) * temp.x;
        return v3;
    }
}