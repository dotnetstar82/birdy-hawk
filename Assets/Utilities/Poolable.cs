using UnityEngine;
using UnityBasics;

/// <summary>
/// Component for objects being used by <c>GameObjectPool</c>. Do not add this
/// behaviour manually, it will be added to any <c>GameObject</c> instantiated
/// by <c>GameObjectPool</c>.
/// </summary>
public class Poolable : MonoBehaviour
{
    public GameObjectPool Pool;

    /// <summary>
    /// Return the <c>GameObject</c> to its pool. This method should be called instead of
    /// Destroying the <c>GameObject</c>.
    /// </summary>
    public void Repool()
    {
        if( Pool == null )
        {
            Debug.LogError( "Pool is null!" );
            return;
        }
        Pool.PoolObject( gameObject );
    }

#if UNITY_EDITOR
    // The following code should play a warning if an object with a Poolable
    // component is destroyed, but not if the application is played in Editor..
    bool hasQuit = false;

    void OnApplicationQuit()
    {
        hasQuit = true;
    }

    void OnDestroy()
    {
        if( hasQuit == false ) {
            Debug.LogWarning( "Do not Destroy objects with a Poolable component - " +
                    "they should be removed from play with 'Poolable.Repool()'!" );
        }
    }
#endif
}

public static class PoolableGameObjectExtension
{
    public static void Repool( this GameObject go )
    {
        go.Component<Poolable>().Repool();
    }
}

public static class PoolableComponentExtension
{
    public static void Repool( this Component c )
    {
        c.Component<Poolable>().Repool();
    }
}
