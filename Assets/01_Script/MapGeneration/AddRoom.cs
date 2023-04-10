using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplatesScript roomTemplates;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplatesScript>();
        roomTemplates.rooms.Add(gameObject);
    }
}
