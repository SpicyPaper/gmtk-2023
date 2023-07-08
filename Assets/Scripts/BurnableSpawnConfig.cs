using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnableSpawnConfig", menuName = "Config/BurnableSpawnConfig")]
public class BurnableSpawnConfig : ScriptableObject
{
    public List<LevelSpawnConfig> levelSpawnConfigs;
}

[System.Serializable]
public class LevelSpawnConfig
{
    public GameState level;
    public List<BurnableSpawnChance> burnableSpawnChances;
    public int numberOfBurnables;
    public float spawnInterval = 4.0f;
}

[System.Serializable]
public class BurnableSpawnChance
{
    public GameObject burnablePrefab;
    public float spawnProbability;
}