using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplatesScript : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;

    public List<GameObject> rooms;

    [SerializeField]
    private GameObject baseRoom;

    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject firstRoomIndicator;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private int minRoomCount;


    private void Start()
    {
        StartCoroutine(waitForGenerated());
    }

    private IEnumerator waitForGenerated()
    {
        yield return new WaitForSeconds(waitTime);
        if(rooms.Count < minRoomCount)
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
}
