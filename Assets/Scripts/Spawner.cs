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

    private float spawnTimer;

    private LevelSpawnConfig levelConfig;

    private int deepshit;
    private bool doneOnce;

    private void Start()
    {
        levelConfig = spawnConfig.levelSpawnConfigs.Find(config => config.level == GameController.gameState);
        if (levelConfig != null)
        {
            spawnTimer = levelConfig.spawnInterval;
        }
    }

    void FixedUpdate()
    {
        TrySpawnBurnable();
    }

    private void TrySpawnBurnable()
    {
        // Generate a random number between 0 and sum of probabilities in burnablePrefabs
        levelConfig = spawnConfig.levelSpawnConfigs.Find(config => config.level == GameController.gameState);

        if (levelConfig != null)
        {
            if (!doneOnce)
            {
                doneOnce = true;
                SoundHandler.Instance.PlaySound(SoundHandler.SoundType.MAIN_MUSIC);
                SoundHandler.Instance.PlaySound(SoundHandler.SoundType.FIRE);
            }

            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer > 0)
            {
                return;
            }

            if (deepshit % 2 == 0)
            {
                SoundHandler.Instance.PlayRegisteredSounds();
            }
            deepshit++;

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
            spawnTimer = levelConfig.spawnInterval + spawnTimer;
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
