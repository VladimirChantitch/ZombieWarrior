using character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Transform Hierarchy;
    [SerializeField] float timer;
    [SerializeField] short max_spawn;
    short currentAmount;
    float time;

    public bool canSpawn = false;

    public event Action onZombieKilled;

    private void Update()
    {
        if (canSpawn)
        {
            SpawnAnEnemy();
        }
    }

    private void SpawnAnEnemy()
    {
        if (time + timer < Time.time)
        {
            time = Time.time;
            if (currentAmount < max_spawn)
            {
                currentAmount += 1;
                NPCManager nPCManager = Instantiate(EnemyPrefab, transform.position, transform.rotation).GetComponent<NPCManager>();
                nPCManager.transform.SetParent(Hierarchy, true);
                nPCManager.onNpcDied += () => HandleAnNpcDeath();
            }
        }
    }

    private void HandleAnNpcDeath()
    {
        currentAmount -= 1;
        onZombieKilled?.Invoke();
    }
}
