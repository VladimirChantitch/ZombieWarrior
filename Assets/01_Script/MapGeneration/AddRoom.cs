using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplatesScript roomTemplates;

    private void Start()
    {
        roomTemplates = RoomTemplatesScript.Instance;
        roomTemplates.rooms.Add(gameObject);
    }
}
