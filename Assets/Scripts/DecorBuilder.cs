using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject decorPrefab;

    [SerializeField]
    private Vector3 spawnCenter = Vector3.zero;

    [SerializeField]
    private float spawnDistance = 40.0f;

    [SerializeField]
    private int maxDecor = 40;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxDecor; i++){
            SpawnSingleDecor();
        }
    }

    private void SpawnSingleDecor()
    {
        GameObject decor = Instantiate(decorPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomAngle = UnityEngine.Random.Range(0.0f, 360.0f);
        Vector3 randomDirection = Quaternion.Euler(0.0f, randomAngle, 0.0f) * Vector3.forward;
        Vector3 spawnPosition = spawnCenter + randomDirection * spawnDistance;
        return spawnPosition;
    }
}
