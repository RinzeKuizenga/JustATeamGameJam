using UnityEngine;

public class Interact : MonoBehaviour
{
    Transform player;
    public float distanceToInteract = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector2.Distance(transform.position, player.position) < distanceToInteract) 
            {
                //done
            }
        }
    }
}
