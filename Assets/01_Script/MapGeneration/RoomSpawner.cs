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

    private RoomTemplatesScript templates;
    private int rdm;
    private bool hasSpawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplatesScript>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (!hasSpawned)
        {
            switch (openingDirection)
            {
                case Direction.TOP:
                    //Need to spawn room with BOTTOM door
                    rdm = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rdm], transform.position, templates.bottomRooms[rdm].transform.rotation);
                    break;
                case Direction.RIGHT:
                    //Need to spawn room with LEFT door
                    rdm = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rdm], transform.position, templates.leftRooms[rdm].transform.rotation);
                    break;
                case Direction.BOTTOM:
                    //Need to spawn room with TOP door
                    rdm = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rdm], transform.position, templates.topRooms[rdm].transform.rotation);
                    break;
                case Direction.LEFT:
                    //Need to spawn room with RIGHT door
                    rdm = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rdm], transform.position, templates.rightRooms[rdm].transform.rotation);
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
                    Instantiate(templates.closedRoom, transform.position, templates.closedRoom.transform.rotation);
                }
                hasSpawned = true;
                Destroy(gameObject);
            }
            catch (NullReferenceException)
            {
                if (!hasSpawned)
                {
                    hasSpawned = true;
                    RepareDoorOnWalls(other.GetComponent<RoomScript>().directions);
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
        GameObject parentRoom = gameObject.transform.parent.gameObject;
        List<Direction> roomDirs = parentRoom.GetComponentInChildren<RoomScript>().directions;
        roomDirs.Remove(openingDirection);
        GameObject[] templatesRoomSide = templates.topRooms;

        switch (roomDirs[0])
        {
            case Direction.TOP:
                templatesRoomSide = templates.topRooms;
                break;
            case Direction.BOTTOM:
                templatesRoomSide = templates.bottomRooms;
                break;
            case Direction.LEFT:
                templatesRoomSide = templates.leftRooms;
                break;
            case Direction.RIGHT:
                templatesRoomSide = templates.rightRooms;
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
        Destroy(parentRoom);
    }
}
