using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityBasics;

public class GameObjectPool
{
    readonly GameObject prefab;
    Stack<GameObject> pooledObjects;
    Transform parent;

    /// <summary>
    /// Create a new <c>GameObjectPool</c>.
    /// </summary>
    /// <param name="prefab">The prefab to be instantiated by pool.</param>
    /// <param name="initialCount">Initial size of object pool.</param>
    /// <param name="parent">An unscaled parent transform to assign to all created objects.</param>
    public GameObjectPool( GameObject prefab, int initalCount = 0, Transform parent = null)
    {
        this.prefab = prefab;
        this.pooledObjects = new Stack<GameObject>();
        this.parent = parent;

        if( initalCount > 0 )
        {
            PoolObjects( RequestObjects( initalCount ) );
        }
    }

    /// <summary>
    /// Returns an object from the pool or creates a new object if one does not
    /// exist.
    /// </summary>
    public GameObject RequestObject()
    {
        GameObject result;

        if ( pooledObjects.Count == 0 ) {
            result = CreateObject();
        } else {
            result = pooledObjects.Pop();
            result.transform.localScale = prefab.transform.localScale;
            result.SetActive( true );
        }

        return result;
    }

    /// <summary>
    /// Returns an array of objects from the pool, creating any if the pool is
    /// exhausted.
    /// </summary>
    /// <param name="count">Number of objecs to return.</param>
    public IList<GameObject> RequestObjects( int count )
    {
        if( count >= 0 ) Debug.LogError( "Object count must be positive: " + count );

        return Enumerable
            .Repeat<GameObject>( null, count )
            .Select( o => RequestObject() )
            .ToList();
    }

    /// <summary>
    /// Request object, return a reference to one of its components.
    /// </summary>
    public C RequestObject<C>() where C : Component
    {
        GameObject go = RequestObject();
        return go.Component<C>();
    }

    /// <summary>
    /// Request object, but return a reference to one of its components.
    /// </summary>
    public IList<C> RequestObjects<C>( int count ) where C : Component
    {
        return RequestObjects( count ).Select( o => o.Component<C>() ).ToList();
    }

    /// <summary>
    /// Return object to the object pool.
    /// </summary>
    public void PoolObject( GameObject go )
    {
        go.SetActive( false );
        go.transform.parent = parent;
        pooledObjects.Push( go );
    }

    /// <summary>
    /// Return objects to the object pool.
    /// </summary>
    public void PoolObjects( IEnumerable<GameObject> objects )
    {
        foreach( var o in objects )
        {
            PoolObject( o );
        }
    }

    /// <summary>
    /// Create an instance of an object to be managed by the object pool. A
    /// <c>Poolable</c> component will be added to the instance if the prefab
    /// does not provide one.
    /// </summary>
    GameObject CreateObject()
    {
        GameObject go = Object.Instantiate( prefab ) as GameObject;
        Poolable poolable = go.GetComponent<Poolable>();
        if( poolable == null ) {
            poolable = go.AddComponent<Poolable>();
        }
        poolable.Pool = this;
        go.transform.parent = parent;
        return go;
    }
}
