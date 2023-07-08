using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject burnablePrefab;

    [SerializeField]
    private float spawnInterval = 1.0f;

    [SerializeField]
    private Vector3 spawnCenter = Vector3.zero;

    [SerializeField]
    private float spawnDistance = 20.0f;

    private float spawnTimer = 0.0f;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0.0f;
            SpawnBurnable();
        }
    }

    private void SpawnBurnable()
    {
        GameObject burnable = Instantiate(
            burnablePrefab,
            GetRandomSpawnPosition(),
            Quaternion.identity
        );
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomAngle = Random.Range(0.0f, 360.0f);
        Vector3 randomDirection = Quaternion.Euler(0.0f, randomAngle, 0.0f) * Vector3.forward;
        Vector3 spawnPosition = spawnCenter + randomDirection * spawnDistance;
        return spawnPosition;
    }
}
