using UnityEngine;
using UnityBasics;

public class BirdRotationController : Behavior
{
    void LateUpdate()
    {
        var dir = transform.parent.rotation.eulerAngles.x;
        print( (dir - 270f) );
    }
}