using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityBasics;
using System.Linq;

public class Player : Behavior
{
    public int Number = 0;
    public Material Material;

    public Color Color
    {
        get
        {
            return Material.color;
        }
    }

    public int TrailLayer
    {
        get
        {
            return LayerMask.NameToLayer( "Trail " + Number );
        }
    }

    public int BirdLayer
    {
        get
        {
            return LayerMask.NameToLayer( "Bird " + Number );
        }
    }

    Collider _collider;

    void Awake()
    {
        _collider = SelfDescendants().Component<Collider>();
    }

    void Start()
    {
        foreach( var go in Descendants().Select( t => t.gameObject ) )
        {
            go.layer = BirdLayer;
        }

        if( Descendants().Components<Collider>().Count() > 1 )
        {
            Debug.LogError( "bird has too many colliders!", this );
        }
    }

    public Collider Collider
    {
        get
        {
            return _collider;
        }
    }
}
