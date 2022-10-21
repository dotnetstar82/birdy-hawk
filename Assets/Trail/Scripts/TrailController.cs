using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using UnityBasics;

public class TrailController : Behavior
{
    public float MinimumSeparation = 2f;
    public float Period = 0.05f;
    public float Timeout = 1f;

    TrailRenderer _trail;
    GameObjectPool _pool;

    TrailDetector _lastDetector = null;
    int _layer;
    Player _player;

    void Awake()
    {
        _player = Parent().Component<Player>();
        _layer = _player.TrailLayer;
    }

    void Start()
    {
        _trail = Component<TrailRenderer>();
        _pool = Scene.Object<TrailDetectorPool>().Pool;

        _lastDetector = _pool.RequestObject<TrailDetector>();
        _lastDetector.gameObject.layer = _layer;
    }

    void Update()
    {
        var distance = Vector3.Distance(
            _lastDetector.transform.position,
            transform.position
        );

        if( distance > MinimumSeparation )
        {
            var nextPosition = transform.position;
            _lastDetector.transform.LookAt( nextPosition );
            _lastDetector.transform.localScale = _lastDetector.transform.localScale.WithZ( distance );

            var detector = _pool.RequestObject<TrailDetector>();
            detector.transform.position = transform.position;
            detector.Initialize( _player, Timeout );
            detector.gameObject.layer = _layer;

            _lastDetector.Next = detector;

            _lastDetector = detector;
        }
    }
}
