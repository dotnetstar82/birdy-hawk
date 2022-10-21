using UnityEngine;

public class TrailDetectorPool : MonoBehaviour
{
    public GameObject Prefab;
    public GameObjectPool Pool;

    void Awake()
    {
        Pool = new GameObjectPool( Prefab, 0, transform );
    }
}