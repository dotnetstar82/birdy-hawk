using UnityEngine;
using UnityBasics;
using System.Collections.Generic;
using System.Linq;

public class TrailDetector : Behavior
{
    public TrailDetector Next = null;
    public int MinimumLoopCount = 10;

    Player _owner;

    public void Initialize( Player owner, float duration )
    {
        _owner = owner;
        Physics.IgnoreCollision( collider, owner.Collider, true );
        Invoke( "RestoreCollision", 0.5f );
        Invoke( "Repool", duration );
    }

    void RestoreCollision()
    {
        Physics.IgnoreCollision( collider, _owner.Collider, false );
        //SelfChildren().Component<Renderer>().material.color = Color.yellow;
    }

    void Repool()
    {
        Component<Poolable>().Repool();
    }

    void OnDisable()
    {
        Next = null;
        //Children().Component<Renderer>().material.color = Color.white;
    }

    void OnTriggerEnter( Collider c )
    {
        var descendants = Descendants().ToArray();
        if( descendants.Length < MinimumLoopCount )
        {
            return;
        }

        var flowersInLoop = Scene.Object<FlowerManager>().Flowers
            .Where( f => PointInLoop( f.transform.position, descendants ) );

        foreach( var flower in flowersInLoop )
        {
            flower.SetOwner( 0 );
        }
    }

    bool PointInLoop( Vector3 point, IEnumerable<TrailDetector> colliders )
    {
        int originalLayer = gameObject.layer;
        int loopTestLayer = LayerMask.NameToLayer( "LoopTest" );

        SetLayer( colliders, loopTestLayer );
        //SetColor( colliders, Color.Lerp( Color.blue, Color.red, Random.value ) );

        int count = 0;

        var directions = new [] {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        foreach( var dir in directions )
        {
            RaycastHit hit;
            if( Physics.Linecast(
                point + dir * 100f,
                point,
                out hit,
                1 << loopTestLayer
            ) )
            {
                count++;
            }

            //Debug.DrawRay( point + dir * 100f, -dir * 100f, Color.red, 2f );
        }

        SetLayer( colliders, originalLayer );

        return count == 4;
    }

    void SetColor( IEnumerable<TrailDetector> components, Color color )
    {
        foreach( var c in components )
        {
            c.Children().Component<Renderer>().material.color = color;
        }
    }

    void SetLayer( IEnumerable<TrailDetector> components, int layer )
    {
        foreach( var c in components )
        {
            c.gameObject.layer = layer;
        }
    }

    IEnumerable<TrailDetector> Descendants()
    {
        var iter = this;
        while( iter != null )
        {
            yield return iter;
            iter = iter.Next;
        }
    }
}