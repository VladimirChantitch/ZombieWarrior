using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    [Header("Direction")]
    public Direction openingDirection;

    [Header("Other params")]
    private RoomGenerationManager roomManager;
    private RoomScript roomScript;
    private int rdm;
    private bool hasSpawned = false;

    private void Awake()
    {
        roomManager = GetComponentInParent<RoomScript>().roomManager;
        roomScript = GetComponentInParent<RoomScript>();
    }

    #region spawn
    public void Spawn()
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
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
        }
    }
    #endregion

    #region connectChecker
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            if (!CheckIfConnected(other.gameObject))
            {
                roomManager.ReplaceRoom(roomScript, other.GetComponent<RoomScript>(), openingDirection);
            }
            else
            {
                hasSpawned = true;
                Destroy(gameObject);
            }
        }
    }

    private bool CheckIfConnected(GameObject gameObject)
    {
        List<Door> doorsDirection = gameObject.GetComponent<RoomScript>().doors;
        foreach (Door door in doorsDirection)
        {
            if(roomScript.InverseDoor(openingDirection) == door.direction)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
