using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;

public class Flower : MonoBehaviour
{
    public void SetOwner( int number )
    {
        renderer.material.color = renderer.material.color == Color.green
            ? Color.red : Color.green;
    }
}
