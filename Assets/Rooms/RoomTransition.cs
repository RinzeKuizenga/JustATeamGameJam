using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject newRoom;
    public GameObject oldRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        var playerT = collision.transform;
        Destroy(oldRoom);
        var room = Instantiate(newRoom);
        playerT.position = room.transform.Find("Spawn").position + Vector3.back;
    }
}
