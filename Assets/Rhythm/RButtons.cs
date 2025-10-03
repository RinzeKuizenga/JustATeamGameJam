using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class RButtons : MonoBehaviour
{
    private InputActions input = null;
    private float timer = 0f;
    public float size = 1;
    public float delay = 1f; //Must be over 0, else it'll always be perfect score
    //delay for great score, perfect is below 0.5 of it, it's in seconds
    void Awake()
    {
        input = new InputActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.UI.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        input.Disable();
        input.UI.Click.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext value)
    {
        Vector2 v = Mouse.current.position.ReadValue();
        float x_click = v.x;
        float y_click = v.y;
        float click_dist = Mathf.Sqrt((gameObject.transform.position.x - x_click) * (gameObject.transform.position.x - x_click) + (gameObject.transform.position.y - y_click) * (gameObject.transform.position.y - y_click));
        int score;

        if (click_dist <= size*0.5)
        {
            score=0;
        }
        else if (click_dist <= size)
        {
            score = 1;
        }
        else if (click_dist <= size*1.3)
        {
            score = 2;
        }
        else if (click_dist <= size*1.7)
        {
            score = 3;
        }
        else if (click_dist <= size*2.5)
        {
            score = 4;
        }
        else
        {
            score = 5;
        }

        if (timer <= delay*0.5) 
        {
            //score+=0;
        }
        else if (timer <= delay)
        {
            score += 1;
        }
        else if (timer <= delay*1.25)
        {
            score += 2;
        }
        else if (timer <= delay*1.5)
        {
            score += 3;
        }
        else if (timer <= delay*2)
        {
            score += 4;
        }
        else
        {
            score += 5;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3 * delay)
        {
            Destroy(gameObject);//destroyed after 10 secs
        }
        // Here we want to check if there's clicks
        // and, if there are, we want the distance
        // of these compared to this button.

    }
}
