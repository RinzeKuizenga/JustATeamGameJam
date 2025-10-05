using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject newRoom;
    public GameObject oldRoom;
    public bool unlocked = true;
    public string soundName = "DoorOpen";
    public string spawnName = "Spawn";
    public string roomType = "Carpet";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;

        if (!unlocked)
            return;

        Destroy(oldRoom);
        var room = Instantiate(newRoom);

        Sounds.PlayClip(soundName);
        Sounds.ChangeFoot("Foot" + roomType);

        collider.transform.position = room.transform.Find(spawnName).position + Vector3.back;
        var comps = newRoom.GetComponentsInChildren<Spawnable>(true);

        foreach (var comp in comps)
        {
            var move = collider.GetComponent<Move>();
            if (move.seenDialogId.Contains(comp.spawnDialogId))
                comp.gameObject.SetActive(true);
            else
                comp.gameObject.SetActive(false);
        }
    }
}