using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityBasics;
using System;

public class BirdAnimationController : Behavior
{
    void Awake()
    {
        Parent().Component<BirdMovement>().FlapEvent += FlapHandler;
    }

    void Start()
    {
        animation[ "bird_sit_idle" ].layer = 0;
        animation[ "bird_dive" ].layer = 0;
        animation[ "bird_glide" ].layer = 0;

        animation[ "bird_flap" ].layer = 1;
        animation[ "bird_flap" ].speed = 2f;

        animation.Play( "bird_dive" );
    }

    void FlapHandler( BirdMovement movement )
    {
        animation.Play( "bird_flap" );
        animation[ "bird_flap" ].normalizedTime = 0f;

        /*
        if( _tween != null )
        {
            _tween.destroy();
        }

        transform.localScale = new Vector3( 2f, 3f, 2f );
        _tween = Go.to( transform, 0.2f, new GoTweenConfig()
            .scale( Vector3.one )
            .setEaseType( GoEaseType.SineOut )
        );
        */
    }
}
