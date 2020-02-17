using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidConfig
{
    [SerializeField] public GameObject asteroidPrefab;
    [SerializeField] public int[] possibleTiers;
    [Range(0.00f, 1.00f)]
    [SerializeField] public float spawnChance;
}
