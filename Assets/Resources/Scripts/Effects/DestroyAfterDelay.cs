using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [Range(0, 10)] public float Delay;

    private void Start()
    {
        Destroy(gameObject, Delay);
    }
}
