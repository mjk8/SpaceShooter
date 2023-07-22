using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 1f;
    private WaveConfigSO currentWave;
    [SerializeField] private bool isLooping = true;
    
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            for (int i = 0; i < waveConfigs.Count; i++)
            {
                currentWave = waveConfigs[i];
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(j),
                        currentWave.GetStartingWayPoint().position,
                        quaternion.identity, transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }

    public WaveConfigSO GetCurrentWaveConfigSo()
    {
        return currentWave;
    }
}