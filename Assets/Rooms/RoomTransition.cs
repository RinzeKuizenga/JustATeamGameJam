using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject newRoom;
    public GameObject oldRoom;
    public string spawnName = "Spawn";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;

        Destroy(oldRoom);
        var room = Instantiate(newRoom);
        collider.transform.position = room.transform.Find(spawnName).position + Vector3.back;
    }
}
