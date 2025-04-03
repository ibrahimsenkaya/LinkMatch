using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper 
{
    public static float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
    public static Vector3 Change(Vector3 org, object x = null, object y = null, object z = null) {
        return new Vector3( (x==null? org.x: (float)x), (y==null? org.y:(float)y), (z==null? org.z: (float)z) );
    }
}
