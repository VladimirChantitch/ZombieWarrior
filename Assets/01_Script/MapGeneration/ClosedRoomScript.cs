using System.Collections;
using UnityEngine;

public class ClosedRoomScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(delayedCollider());
    }

    IEnumerator delayedCollider()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
