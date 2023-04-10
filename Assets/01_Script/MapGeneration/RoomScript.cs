using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<Direction> directions = new List<Direction>();

    private void Awake()
    {
        GameObject room = gameObject.transform.parent.gameObject;
        foreach (var door in room.GetComponentsInChildren<RoomSpawner>())
        {
            directions.Add(door.openingDirection);
        }
    }
}
