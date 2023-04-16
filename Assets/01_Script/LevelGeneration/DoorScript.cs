using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private RoomScript room;
    private void Start()
    {
        room = GetComponentInParent<RoomScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            room.Spawn();
        }
    }
}
