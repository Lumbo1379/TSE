using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergyOrb : MonoBehaviour
{
    [Range(0, 10)] public float SpawnRate;
    public GameObject EnergyOrb;

    private void Start()
    {
        InvokeRepeating("SpawnOrb", 1f, SpawnRate);
    }

    private void SpawnOrb()
    {
        Instantiate(EnergyOrb, transform);
    }
}
