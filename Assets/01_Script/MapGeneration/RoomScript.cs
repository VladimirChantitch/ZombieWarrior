using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<RoomSpawner> spawners = new List<RoomSpawner>();

    public RoomGenerationManager roomManager;

    private void Awake()
    {
        roomManager = RoomGenerationManager.Instance;
    }

    private void Start()
    {
        //StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        foreach (RoomSpawner spawner in spawners)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public List<Direction> GetDirections()
    {
        List<Direction> directions = new List<Direction>();
        foreach (RoomSpawner spawner in spawners)
        {
            directions.Add(spawner.openingDirection);
        }
        return directions;
    }
}