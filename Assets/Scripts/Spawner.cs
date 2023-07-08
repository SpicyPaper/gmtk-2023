using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public BurnableSpawnConfig spawnConfig;

    [SerializeField]
    private Vector3 spawnCenter = Vector3.zero;

    [SerializeField]
    private float spawnDistance = 20.0f;

    private float spawnTimer = 0.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        TrySpawnBurnable();
    }


    private void TrySpawnBurnable()
    {
        // Generate a random number between 0 and sum of probabilities in burnablePrefabs
        LevelSpawnConfig levelConfig = spawnConfig.levelSpawnConfigs.Find(config => config.level == GameController.gameState);

        if (levelConfig != null)
        {

            spawnTimer += Time.fixedDeltaTime;
            if (spawnTimer < levelConfig.spawnInterval)
            {
                return;
            }


            for (int i = 0; i < levelConfig.numberOfBurnables; i++)
            {
                float probSum = levelConfig.burnableSpawnChances.Sum(x => x.spawnProbability);
                float randomNum = Random.Range(0.0f, probSum);
                foreach (var item in levelConfig.burnableSpawnChances)
                {
                    if (randomNum < item.spawnProbability)
                    {
                        SpawnBurnable(item.burnablePrefab);
                        break;
                    }
                    else
                    {
                        randomNum -= item.spawnProbability;
                    }
                }
            }
            spawnTimer = 0.0f;

        }

    }

    private void SpawnBurnable(GameObject burnable)
    {
        Instantiate(
            burnable,
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
