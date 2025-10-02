using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RButtons : MonoBehaviour
{
    private InputActions input = null;
    private int x_click=0;
    private int y_click=0;
    private float timer = 0f;

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
        x_click = 0;
        y_click = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // We'll initialize our variables here
        // Except public ones
        input = new InputActions();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            Destroy(gameObject);//destroyed after 10 secs
        }
        // Here we want to check if there's clicks
        // and, if there are, we want the distance
        // of these compared to this button.

    }
}
