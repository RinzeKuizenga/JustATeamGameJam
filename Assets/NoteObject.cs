using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    private bool scored = false;
    private string currentHitType = ""; // "Perfect" or "Good"

    void Update()
    {
        if (!scored && Input.GetKeyDown(keyToPress) && canBePressed)
        {
            scored = true;

            if (currentHitType == "Perfect")
            {
                GameManager.instance.PerfectHit();
            }
            else if (currentHitType == "Good")
            {
                GameManager.instance.GoodHit();
            }

            gameObject.SetActive(false); // prevent exit from triggering after hit
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PerfectActivator"))
        {
            canBePressed = true;
            currentHitType = "Perfect";
        }
        else if (other.CompareTag("GoodActivator"))
        {
            canBePressed = true;
            currentHitType = "Good";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // only miss if the note was never hit
        if (!scored && (other.CompareTag("PerfectActivator") || other.CompareTag("GoodActivator")))
        {
            canBePressed = false;
            scored = true; // prevent multiple calls
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
        }
    }
}
