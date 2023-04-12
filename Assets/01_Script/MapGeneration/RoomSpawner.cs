using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public Direction openingDirection;
    // TOP --> need BOTTOM door
    // BOTTOM --> need TOP door
    // LEFT --> need RIGHT door
    // RIGHT --> need LEFT door

    private RoomGenerationManager roomManager;
    private int rdm;
    private bool hasSpawned = false;

    private void Awake()
    {
        roomManager = GetComponentInParent<RoomScript>().roomManager;
        Invoke("Spawn", 0.1f);
    }

    public void Spawn()
    {
        if (!hasSpawned)
        {
            switch (openingDirection)
            {
                case Direction.TOP:
                    //Need to spawn room with BOTTOM door
                    rdm = Random.Range(0, roomManager.bottomRooms.Length);
                    roomManager.addRoom(roomManager.bottomRooms[rdm], transform.position);
                    break;
                case Direction.RIGHT:
                    //Need to spawn room with LEFT door
                    rdm = Random.Range(0, roomManager.leftRooms.Length);
                    roomManager.addRoom(roomManager.leftRooms[rdm], transform.position);
                    break;
                case Direction.BOTTOM:
                    //Need to spawn room with TOP door
                    rdm = Random.Range(0, roomManager.topRooms.Length);
                    roomManager.addRoom(roomManager.topRooms[rdm], transform.position);
                    break;
                case Direction.LEFT:
                    //Need to spawn room with RIGHT door
                    rdm = Random.Range(0, roomManager.rightRooms.Length);
                    roomManager.addRoom(roomManager.rightRooms[rdm], transform.position);
                    break;
            }
            hasSpawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try
            {
                if (!other.GetComponent<RoomSpawner>().hasSpawned && !hasSpawned)
                {
                    Instantiate(roomManager.closedRoom, transform.position, roomManager.closedRoom.transform.rotation);
                }
                hasSpawned = true;
                Destroy(gameObject);
            }
            catch (NullReferenceException)
            {
                if (!hasSpawned)
                {
                    hasSpawned = true;
                    RepareDoorOnWalls(other.GetComponent<RoomScript>().GetDirections());
                }
            }
        }
    }

    void RepareDoorOnWalls(List<Direction> other)
    {
        switch (openingDirection)
        {
            case Direction.TOP:
                if (!other.Contains(Direction.BOTTOM))
                {
                    ReplaceRoom();
                }
                break;
            case Direction.BOTTOM:
                if (!other.Contains(Direction.TOP))
                {
                    ReplaceRoom();
                }
                break;
            case Direction.LEFT:
                if (!other.Contains(Direction.RIGHT))
                {
                    ReplaceRoom();
                }
                break;
            case Direction.RIGHT:
                if (!other.Contains(Direction.LEFT))
                {
                    ReplaceRoom();
                }
                break;
        }
    }

    void ReplaceRoom()
    {
        GameObject parentRoom = transform.parent.gameObject;
        List<Direction> roomDirs = GetComponentInParent<RoomScript>().GetDirections();
        roomDirs.Remove(openingDirection);
        GameObject[] templatesRoomSide = new GameObject[0];

        switch (roomDirs[0])
        {
            case Direction.TOP:
                templatesRoomSide = roomManager.topRooms;
                break;
            case Direction.BOTTOM:
                templatesRoomSide = roomManager.bottomRooms;
                break;
            case Direction.LEFT:
                templatesRoomSide = roomManager.leftRooms;
                break;
            case Direction.RIGHT:
                templatesRoomSide = roomManager.rightRooms;
                break;
        }

        //Replace current room
        foreach (var room in templatesRoomSide)
        {
            List<Direction> wantedRoomDirs = new List<Direction>();
            foreach (var dir in room.GetComponentsInChildren<RoomSpawner>())
            {
                wantedRoomDirs.Add(dir.openingDirection);
            }

            bool isEqual = Enumerable.SequenceEqual(roomDirs.OrderBy(e => e), wantedRoomDirs.OrderBy(e => e));

            if (isEqual)
            {
                var obj = Instantiate(room, parentRoom.transform.position, room.transform.rotation);
                break;
            }
        }
        roomManager.CleanRoomList(parentRoom);
        Destroy(parentRoom);
    }
}
