using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityBasics;
using System.Linq;

public class FlowerManager : Behavior
{
    public Flower[] Flowers { get; private set; }

    void Start()
    {
        Flowers = Scene.Objects<Flower>().ToArray();
    }
}
