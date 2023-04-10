using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSelectorScript : MonoBehaviour
{
    /*[SerializeField]
    private string sceneGroupPath;*/

    [SerializeField]
    public GameObject roomDoorTop;
    [SerializeField]
    public GameObject roomDoorBottom;
    [SerializeField]
    public GameObject roomDoorRight;
    [SerializeField]
    public GameObject roomDoorLeft;

    [SerializeField]
    private GameObject[] roomPrefabs;

    public GameObject roomNextToTop;
    public GameObject roomNextToBottom;
    public GameObject roomNextToRight;
    public GameObject roomNextToLeft;

    //private List<Scene> scenes;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (string scene in Directory.GetFiles(sceneGroupPath))
        {
            if (scene.Contains(".unity.meta"))
            {
                SceneManager.LoadScene(scene.Split(".")[0]);
                scenes.Add(SceneManager.GetSceneByPath(scene.Split(".")[0]+"."+scene.Split(".")[1]));
            }
        }*/
    }

    public void GenerateRoom(string doorSide)
    {
        List<GameObject> validCaseRooms = new List<GameObject>();
        foreach(GameObject room in roomPrefabs)
        {
            switch (doorSide)
            {
                case "top":
                    if (room.GetComponent<RoomSelectorScript>().roomDoorBottom != null)
                    {
                        validCaseRooms.Add(room);
                    }
                    break;
                case "bottom":
                    if (room.GetComponent<RoomSelectorScript>().roomDoorTop != null)
                    {
                        validCaseRooms.Add(room);
                    }
                    break;
                case "right":
                    if (room.GetComponent<RoomSelectorScript>().roomDoorLeft != null)
                    {
                        validCaseRooms.Add(room);
                    }
                    break;
                case "left":
                    if (room.GetComponent<RoomSelectorScript>().roomDoorRight != null)
                    {
                        validCaseRooms.Add(room);
                    }
                    break;
            }
        }
        GameObject roomPrefab = validCaseRooms[Random.Range(0, validCaseRooms.Count)];
        GameObject newRoom = null;
        switch (doorSide)
        {
            case "top":
                newRoom = Instantiate(roomPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                    roomDoorTop.transform.position.z + roomPrefab.GetComponent<SpriteRenderer>().bounds.size.z), gameObject.transform.rotation);
                roomNextToTop = newRoom;
                newRoom.GetComponent<RoomSelectorScript>().roomNextToBottom = gameObject;
                break;
            case "bottom":
                newRoom = Instantiate(roomPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                    roomDoorBottom.transform.position.z - roomPrefab.GetComponent<SpriteRenderer>().bounds.size.z), gameObject.transform.rotation);
                roomNextToBottom = newRoom;
                newRoom.GetComponent<RoomSelectorScript>().roomNextToTop = gameObject;
                break;
            case "right":
                newRoom = Instantiate(roomPrefab, new Vector3(roomDoorRight.transform.position.x + roomPrefab.GetComponent<SpriteRenderer>().bounds.size.x, 
                    gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
                roomNextToRight = newRoom;
                newRoom.GetComponent<RoomSelectorScript>().roomNextToLeft = gameObject;
                break;
            case "left":
                newRoom = Instantiate(roomPrefab, new Vector3(roomDoorLeft.transform.position.x - roomPrefab.GetComponent<SpriteRenderer>().bounds.size.x, 
                    gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
                roomNextToLeft = newRoom;
                newRoom.GetComponent<RoomSelectorScript>().roomNextToRight = gameObject;
                break;
        }
        ManageTransition(newRoom, doorSide);
    }

    void SetDoorAlreadyGenerated(string newDoorSide)
    {
        switch (newDoorSide)
        {
            case "top":
                roomDoorTop.GetComponent<DoorScript>().SetRoomGenerated();
                break;
            case "bottom":
                roomDoorBottom.GetComponent<DoorScript>().SetRoomGenerated();
                break;
            case "right":
                roomDoorRight.GetComponent<DoorScript>().SetRoomGenerated();
                break;
            case "left":
                roomDoorLeft.GetComponent<DoorScript>().SetRoomGenerated();
                break;
        }
    }

    private void ManageTransition(GameObject newRoom, string doorSide)
    {
        switch (doorSide)
        {
            case "top":
                TeleportPlayer(newRoom.GetComponent<RoomSelectorScript>().roomDoorBottom.transform.position);
                newRoom.GetComponent<RoomSelectorScript>().SetDoorAlreadyGenerated("bottom");
                break;
            case "bottom":
                TeleportPlayer(newRoom.GetComponent<RoomSelectorScript>().roomDoorTop.transform.position);
                newRoom.GetComponent<RoomSelectorScript>().SetDoorAlreadyGenerated("top");
                break;
            case "right":
                TeleportPlayer(newRoom.GetComponent<RoomSelectorScript>().roomDoorLeft.transform.position);
                newRoom.GetComponent<RoomSelectorScript>().SetDoorAlreadyGenerated("left");
                break;
            case "left":
                TeleportPlayer(newRoom.GetComponent<RoomSelectorScript>().roomDoorRight.transform.position);
                newRoom.GetComponent<RoomSelectorScript>().SetDoorAlreadyGenerated("right");
                break;
        }
        newRoom.SetActive(true);
    }

    public void TeleportPlayer(Vector3 pos)
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(pos.x,GameObject.FindGameObjectWithTag("Player").transform.position.y, pos.z);
    }
}
