using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool roomIsGenerated;

    private bool canTeleport;

    [SerializeField]
    private string doorSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomGenerated()
    {
        roomIsGenerated = true;
    }

    public void SetCantTeleport()
    {
        canTeleport = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!roomIsGenerated && other.CompareTag("Player"))
        {
            SetRoomGenerated();
            GetComponentInParent<RoomSelectorScript>().GenerateRoom(doorSide);
        }
        else if (roomIsGenerated && canTeleport)
        {
            switch (doorSide)
            {
                case "top":
                    GetComponentInParent<RoomSelectorScript>().roomNextToTop.GetComponent<RoomSelectorScript>().roomDoorBottom.GetComponent<DoorScript>().SetCantTeleport();
                    GetComponentInParent<RoomSelectorScript>().TeleportPlayer(
                        GetComponentInParent<RoomSelectorScript>().roomNextToTop.GetComponent<RoomSelectorScript>().roomDoorBottom.transform.position);
                    break;
                case "bottom":
                    GetComponentInParent<RoomSelectorScript>().roomNextToBottom.GetComponent<RoomSelectorScript>().roomDoorTop.GetComponent<DoorScript>().SetCantTeleport();
                    GetComponentInParent<RoomSelectorScript>().TeleportPlayer(
                        GetComponentInParent<RoomSelectorScript>().roomNextToBottom.GetComponent<RoomSelectorScript>().roomDoorTop.transform.position);
                    break;
                case "right":
                    GetComponentInParent<RoomSelectorScript>().roomNextToRight.GetComponent<RoomSelectorScript>().roomDoorLeft.GetComponent<DoorScript>().SetCantTeleport();
                    GetComponentInParent<RoomSelectorScript>().TeleportPlayer(
                        GetComponentInParent<RoomSelectorScript>().roomNextToRight.GetComponent<RoomSelectorScript>().roomDoorLeft.transform.position);
                    break;
                case "left":
                    GetComponentInParent<RoomSelectorScript>().roomNextToLeft.GetComponent<RoomSelectorScript>().roomDoorRight.GetComponent<DoorScript>().SetCantTeleport();
                    GetComponentInParent<RoomSelectorScript>().TeleportPlayer(
                        GetComponentInParent<RoomSelectorScript>().roomNextToLeft.GetComponent<RoomSelectorScript>().roomDoorRight.transform.position);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTeleport = true;
    }
}
