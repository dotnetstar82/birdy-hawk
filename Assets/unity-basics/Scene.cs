using UnityEngine;
using System;

namespace UnityBasics
{

public class SceneObjectNotFoundException : Exception
{
    public SceneObjectNotFoundException( Type objectType )
        : base( string.Format(
            "Could not find object of type {0} in scene.", objectType )
        )
    { }
}

public class Scene
{
    public static GameObject GameObjectOrNull( string name )
    {
        return UnityEngine.GameObject.Find( name );
    }

    public static GameObject GameObject( string name )
    {
        var go = GameObjectOrNull( name );
        if( go == null )
        {
            throw new SceneObjectNotFoundException( typeof(GameObject) );
        }
        return go;
    }

    public static GameObject GameObjectWithTagOrNull( string tag )
    {
        return UnityEngine.GameObject.FindWithTag( tag );
    }

    public static GameObject GameObjectWithTag( string tag )
    {
        var go = GameObjectWithTagOrNull( tag );
        if( go == null )
        {
            throw new SceneObjectNotFoundException( typeof(GameObject) );
        }
        return go;
    }

    public static GameObject[] GameObjectsWithTag( string tag )
    {
        return UnityEngine.GameObject.FindGameObjectsWithTag( tag );
    }

    public static TObject ObjectOrNull<TObject>() where TObject : UnityEngine.Object
    {
        return UnityEngine.Object.FindObjectOfType( typeof(TObject) ) as TObject;
    }

    public static TObject Object<TObject>() where TObject : UnityEngine.Object
    {
        var o = ObjectOrNull<TObject>();
        if( o == null )
        {
            throw new SceneObjectNotFoundException( typeof(TObject) );
        }
        return o;
    }

    public static TObject[] Objects<TObject>() where TObject : UnityEngine.Object
    {
        return UnityEngine.Object.FindObjectsOfType( typeof(TObject) ) as TObject[];
    }
}

}