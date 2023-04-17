using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [Header("Doors")]
    public List<RoomSpawner> spawners = new List<RoomSpawner>();
    public List<Door> doors = new List<Door>();

    [Header("Manager")]
    public RoomGenerationManager roomManager;

    [Header("Other params")]
    private bool hasSpawned = false;

    private void Awake()
    {
        roomManager = RoomGenerationManager.Instance;
    }

    public void Spawn()
    {
        if (!hasSpawned)
        {
            foreach (RoomSpawner spawner in spawners)
            {
                spawner.Spawn();
            }
            hasSpawned = true;
        }
    }

    #region roomDirections
    public Direction InverseDoor(Direction direction)
    {
        switch (direction)
        {
            case Direction.TOP:
                return Direction.BOTTOM;
            case Direction.BOTTOM:
                return Direction.TOP;
            case Direction.RIGHT:
                return Direction.LEFT;
            case Direction.LEFT:
                return Direction.RIGHT;
        }
        return Direction.TOP;
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
    #endregion
}
