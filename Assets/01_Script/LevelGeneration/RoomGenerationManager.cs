using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerationManager : MonoBehaviour
{
    [Header("Rooms to add")]
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [Header("Special rooms")]
    [SerializeField]
    private GameObject baseRoom;

    [Header("Current rooms")]
    private List<GameObject> rooms = new List<GameObject>();

    #region singleton
    public static RoomGenerationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private void Start()
    {
        GameObject firstRoom = Instantiate(baseRoom, Vector3.zero, baseRoom.transform.rotation);
        firstRoom.GetComponent<RoomScript>().Spawn();
    }

    #region roomGeneration
    public void addRoom(GameObject room, Vector3 position)
    {
        rooms.Add(Instantiate(room, position, transform.rotation));
    }

    public void ReplaceRoom(RoomScript roomToDestroy, RoomScript roomToConnect, Direction unwantedDirection)
    {
        List<Direction> wantedDirections = roomToDestroy.GetDirections().FindAll(x => x != unwantedDirection);
        GameObject[] directionsRooms = new GameObject[0];
        Vector3 wantedPos = roomToDestroy.transform.position;
        Destroy(roomToDestroy.gameObject);

        switch (wantedDirections[0])
        {
            case Direction.TOP:
                directionsRooms = topRooms;
                break;
            case Direction.BOTTOM:
                directionsRooms = bottomRooms;
                break;
            case Direction.LEFT:
                directionsRooms = leftRooms;
                break;
            case Direction.RIGHT:
                directionsRooms = rightRooms;
                break;
        }

        foreach (GameObject room in directionsRooms)
        {
            if (CompareRooms(wantedDirections, room.GetComponent<RoomScript>().GetDirections()))
            {
                addRoom(room, roomToDestroy.transform.position);
                break;
            }
        }
        CleanRoomList();
    }

    private bool CompareRooms(List<Direction> A, List<Direction> B)
    {
        if (A.Count != B.Count)
        {
            return false;
        }

        List<Direction> ASort = A.ToList();
        ASort.Sort();
        List<Direction> BSort = B.ToList();
        BSort.Sort();

        for (int i = 0; i < ASort.Count; i++)
        {
            if (ASort[i] != BSort[i])
            {
                return false;
            }
        }

        return true;
    }
    #endregion

    public void CleanRoomList()
    {
        rooms.RemoveAll(x => x == null);
    }
}
