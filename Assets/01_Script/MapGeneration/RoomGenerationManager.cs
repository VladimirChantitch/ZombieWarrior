using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationManager : MonoBehaviour
{
    [Header("Rooms to add")]
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [Header("Special rooms")]
    public GameObject closedRoom;
    [SerializeField]
    private GameObject baseRoom;

    [Header("Current rooms")]
    [SerializeField]
    private List<GameObject> rooms = new List<GameObject>();

    [Header("Indicators")]
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject firstRoomIndicator;

    [Header("Other params")]
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private int maxRooms;
    [SerializeField]
    private int minRooms;

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
        Instantiate(baseRoom, Vector3.zero, baseRoom.transform.rotation);
        StartCoroutine(waitForGenerated());
    }

    private IEnumerator waitForGenerated()
    {
        yield return new WaitForSeconds(waitTime);
        if(rooms.Count < maxRooms)
        {
            foreach (var item in rooms)
            {
                Destroy(item);
            }
            rooms = new List<GameObject>();
            Instantiate(baseRoom, Vector3.zero, baseRoom.transform.rotation);
            StartCoroutine(waitForGenerated());
        }
        else
        {
            //Génère le boss dans la dernière salle & un point bleu dans la première
            Instantiate(firstRoomIndicator, rooms[0].transform.position, rooms[0].transform.rotation);
            Instantiate(boss, rooms[rooms.Count - 1].transform.position, rooms[rooms.Count - 1].transform.rotation);
        }
    }

    public void addRoom(GameObject room, Vector3 position)
    {
        if (rooms.Count < maxRooms)
        {
            rooms.Add(Instantiate(room, position, transform.rotation));
        }
    }

    public void CleanRoomList(GameObject room)
    {
        rooms.Remove(room);
    }
}
