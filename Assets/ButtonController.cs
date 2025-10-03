using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keytoPress;
    public Animator shockAnim;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keytoPress))
        {
            theSR.sprite = pressedImage;
            shockAnim.SetTrigger("Play");
        }

        if (Input.GetKeyUp(keytoPress))
        { 
            theSR.sprite = defaultImage; 
        }
    }
}
