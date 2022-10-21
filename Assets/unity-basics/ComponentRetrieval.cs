using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UnityBasics
{

public class ComponentNotFoundException : InvalidOperationException
{
    public ComponentNotFoundException( Type componentType )
        : base( string.Format(
            "Could not find component of type {0}.", componentType )
        )
    { }
}

public static class ComponentRetrieval
{
    public static TComponent ComponentOrNull<TComponent>( this GameObject gameObject )
        where TComponent : Component
    {
        return gameObject.GetComponent<TComponent>();
    }

    public static TComponent ComponentOrNull<TComponent>( this Component component )
        where TComponent : Component
    {
        return component.GetComponent<TComponent>();
    }

    public static TComponent ComponentOrNull<TComponent>( this IEnumerable<Transform> transforms )
        where TComponent : Component
    {
        return transforms
            .Select( t => t.GetComponent<TComponent>() )
            .FirstOrDefault();
    }

    public static TComponent Component<TComponent>( this GameObject gameObject )
        where TComponent : Component
    {
        var c = gameObject.GetComponent<TComponent>();
        if( c == null ) {
            throw new ComponentNotFoundException( typeof( TComponent ) );
        }
        return c;
    }

    public static TComponent Component<TComponent>( this Component component )
        where TComponent : Component
    {
        var c = component.GetComponent<TComponent>();
        if( c == null ) {
            throw new ComponentNotFoundException( typeof( TComponent ) );
        }
        return c;
    }

    public static TComponent Component<TComponent>( this IEnumerable<Transform> transforms )
        where TComponent : Component
    {
        var c = transforms.ComponentOrNull<TComponent>();
        if( c == null ) {
            throw new ComponentNotFoundException( typeof( TComponent ) );
        }
        return c;
    }

    public static IEnumerable<TComponent> Components<TComponent>( this GameObject gameObject )
        where TComponent : Component
    {
        return gameObject.GetComponents<TComponent>();
    }

    public static IEnumerable<TComponent> Components<TComponent>( this Component component )
        where TComponent : Component
    {
        return component.GetComponents<TComponent>();
    }

    public static IEnumerable<TComponent> Components<TComponent>( this IEnumerable<Transform> transforms )
        where TComponent : Component
    {
        return transforms
            .SelectMany( t => t.GetComponents<TComponent>() );
    }
}

} // namespace UnityBasics