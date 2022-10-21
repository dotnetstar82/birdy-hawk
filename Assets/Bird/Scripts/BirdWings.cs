using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityBasics;
using System;

public class BirdWings : Behavior
{
    GoTween _tween;

    void Awake()
    {
        Parent().Component<BirdMovement>().FlapEvent += FlapHandler;
    }

    void FlapHandler( BirdMovement movement )
    {
        if( _tween != null )
        {
            _tween.destroy();
        }

        transform.localScale = new Vector3( 2f, 3f, 2f );
        _tween = Go.to( transform, 0.2f, new GoTweenConfig()
            .scale( Vector3.one )
            .setEaseType( GoEaseType.SineOut )
        );
    }
}
