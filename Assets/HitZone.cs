using UnityEngine;

public class HitZone : MonoBehaviour
{
    private Note currentNote;

    void OnTriggerEnter2D(Collider2D other)
    {
        currentNote = other.GetComponent<Note>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Note>() == currentNote)
            currentNote = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(currentNote.key))
        {
            Debug.Log("Hit!");
            Destroy(currentNote.gameObject);
            currentNote = null;
        }
    }
}
