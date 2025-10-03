using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    private bool scored = false; // Ensures we only score once

    void Update()
    {
        if (!scored && Input.GetKeyDown(keyToPress) && canBePressed)
        {
            scored = true;               // Mark as scored
            gameObject.SetActive(false); // Hide the note
            GameManager.instance.NoteHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Activator"))
        {
            scored = true; // Mark as scored so it doesn't trigger multiple times
            canBePressed = false;
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false); // Hide the note after missing
        }
    }
}
