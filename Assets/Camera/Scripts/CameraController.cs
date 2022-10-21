using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public List<Transform> Targets;
    public Vector3 Margin;
    public Vector3 MinimumSize = Vector3.one;

    void LateUpdate()
    {
        Bounds targetBounds = new Bounds( Vector3.zero, Margin );
        var center = Average( Targets.Select( t => t.position ).ToArray() );

        Bounds viewBounds = new Bounds( center, MinimumSize );
        foreach( var target in Targets )
        {
            targetBounds.center = target.position;
            viewBounds.Encapsulate( targetBounds );
        }

        SetView( viewBounds );
    }

    void SetView( Bounds bounds )
    {
        var height = Mathf.Max( bounds.size.x / camera.aspect, bounds.size.y );
        var depth = height / ( 2 * Mathf.Tan( camera.fieldOfView / 2 * Mathf.Deg2Rad ) );
        transform.position = bounds.center - Vector3.forward * depth;
    }

    Vector3 Average( params Vector3[] points )
    {
        if( points.Length == 0 )
        {
            return Vector3.zero;
        }

        var total = points.Aggregate( Vector3.zero, (running, point) => running + point );
        return total / points.Length;
    }
}
