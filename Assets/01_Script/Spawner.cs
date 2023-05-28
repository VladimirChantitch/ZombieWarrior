using character;
using Realms;
using savesystem.realm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ui;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Transform Hierarchy;
    [SerializeField] float timer;
    [SerializeField] short max_spawn;
    short currentAmount;
    float time;
    [SerializeField] UI_Manager ui_Manager;
    public bool canSpawn = false;
    private List<Transform> zombiesPosition = new List<Transform>();
    Realm realm;
    public event Action onZombieKilled;

    private void Awake()
    {
        var config = new RealmConfiguration
        {
            ShouldDeleteIfMigrationNeeded = true,
        };
        realm = Realm.GetInstance(config);
        new ZombiePositionCrud(realm);
    }

    private void Update()
    {
        //if (zombiesPosition.Count > 0)
            //Debug.Log(zombiesPosition[0].position);
        if (canSpawn)
        {
            SpawnAnEnemy();
        }
        saveZombiesPosition();
    }

    private void saveZombiesPosition()
    {
        ui_Manager.onSave += () =>
        {
            foreach (Transform zombiePos in zombiesPosition)
            {
                ZombiePositionCrud.Instance.CreateZombiePosition(zombiePos.position.x, zombiePos.position.y);
            }
        };
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
                zombiesPosition.Add(nPCManager.transform);
                Debug.Log(zombiesPosition.Count);
                Debug.Log(zombiesPosition[0].transform.position.x);
            }
        }
    }

    private void HandleAnNpcDeath()
    {
        currentAmount -= 1;
        onZombieKilled?.Invoke();
    }
}
