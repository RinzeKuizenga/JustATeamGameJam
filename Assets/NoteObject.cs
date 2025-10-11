using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    public int lane;

    private bool scored = false;
    public string currentHitType = ""; 

    void Update()
    {
        if (!scored && Input.GetKeyDown(keyToPress) && canBePressed)
        {
            scored = true;

            if (currentHitType == "Perfect")
            {
                GameManager.instance.PerfectHit(lane);
            }
            else if (currentHitType == "Good")
            {
                GameManager.instance.GoodHit(lane);
            }
            else if (currentHitType == "Missed")
            {
                GameManager.instance.NoteMissed(lane);
            }

            gameObject.SetActive(false); 
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
        else if (other.CompareTag("EarlyActivator"))
        {
            canBePressed= true;
            currentHitType = "Missed";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!scored && other.CompareTag("MissActivator"))
        {
            canBePressed = false;
            scored = true;
            GameManager.instance.NoteMissed(lane);
            gameObject.SetActive(false);
        }
    }

}
