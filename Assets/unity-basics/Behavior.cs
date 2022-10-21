using UnityEngine;
using System.Collections.Generic;

namespace UnityBasics
{

/// <summary>
/// A base class for Unity script components that a protected interface for
/// convenient use in scripts.
/// </summary>
public abstract class Behavior : MonoBehaviour
{
    /// <summary>
    /// Get all immediate children of the <c>Transform</c>.
    /// </summary>
    protected IEnumerable<Transform> Children()
    {
        return transform.Children();
    }

    /// <summary>
    /// Get all descendants of the <c>Transform</c>.
    /// </summary>
    protected IEnumerable<Transform> Descendants()
    {
        return transform.Descendants();
    }

    /// <summary>
    /// Get lineage of <c>Transform</c>. ie. its parent, its parent's
    /// parent etc.
    /// </summary>
    protected IEnumerable<Transform> Ancestors()
    {
        return transform.Ancestors();
    }

    protected Transform Parent()
    {
        return transform.parent;
    }

    /// <summary>
    /// Return <c>Transform</c> and all its immediate children.
    /// </summary>
    protected IEnumerable<Transform> SelfChildren()
    {
        return transform.SelfChildren();
    }

    /// <summary>
    /// Return <c>Transform</c> and all its descendants.
    /// </summary>
    protected IEnumerable<Transform> SelfDescendants()
    {
        return transform.SelfDescendants();
    }

    /// <summary>
    /// Return <c>Transform</c> and its ancestors.
    /// </summary>
    protected IEnumerable<Transform> SelfAncestors()
    {
        return transform.SelfAncestors();
    }

    /// <summary>
    /// Get a <c>Component</c> attached to this <c>GameObject</c> or throw a
    /// <c>ComponentNotFoundException</c>.
    /// </summary>
    protected TComponent Component<TComponent>()
        where TComponent : Component
    {
        return gameObject.Component<TComponent>();
    }

    /// <summary>
    /// Get a <c>Component</c> attached to this <c>GameObject</c> or return
    /// null.
    /// </summary>
    protected TComponent ComponentOrNull<TComponent>()
        where TComponent : Component
    {
        return gameObject.ComponentOrNull<TComponent>();
    }

    /// <summary>
    /// Get all <c>Component</c>s of a given type attached to this
    /// <c>GameObject</c>.
    /// </summary>
    protected IEnumerable<TComponent> Components<TComponent>()
        where TComponent : Component
    {
        return gameObject.Components<TComponent>();
    }
}

}