using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 2f;
    public Animator animator;
    public Transform feet;
    public float feetMargin = 0.2f;
    public bool canMove = true;
    private Vector2 moveDirection = Vector2.zero;
    public List<Interactable> interactables=new List<Interactable>();
    //can be changed to gameobjects too, just an extra step to convert them to transforms
    public float distanceToInteract = 10;
    // how far max to activate something
    public confirmBox confirmation;
    public GameObject EToInteract; //Place EBox in this; EBox MUST be in a canva to work
    private Vector3 originalScale = Vector3.zero;

    private void Start()
    {
        originalScale = transform.localScale;
        confirmation.gameObject.SetActive(false);
    }

    void Update()
    {
        bool e = false;
        foreach (Interactable t in interactables)
        {
            if (Vector2.Distance(transform.position, t.transform.position) < distanceToInteract)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EToInteract.SetActive(true);
                    //do something
                    if (t.prefabToLoad != null)
                    {
                        t.prefabToLoad.SetActive(true);
                    }
                    else if (t.sceneToLoad != null)
                    {
                        confirmation.gameObject.SetActive(true);
                        confirmation.sceneToLoad=t.sceneToLoad;
                    }
                    //animate with t.Animate()
                }
                /*
                bool e = false;
                foreach (Interactable i in interactables) 
                { 
                    if (i.gameObject.activeInHierarchy) 
                    {
                        e = true;
                    }
                }
                EToInteract.SetActive(e);*/
            }
            if (t.gameObject.activeInHierarchy)
            {
                e = true;
            }
        }
        EToInteract.SetActive(e);

        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            confirmation.gameObject.SetActive(false);
            EToInteract.SetActive(false) ;
            foreach (Interactable t in interactables)
            {
                if (t.prefabToLoad != null)
                {
                    t.prefabToLoad.SetActive(false);
                }
            }
        }
        EToInteract.SetActive(false);
        moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            moveDirection += speed * Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveDirection += speed * Vector2.right;
        if (Input.GetKey(KeyCode.W))
            moveDirection += speed * Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection += speed * Vector2.down;

        if (moveDirection.x > 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else if (moveDirection.x < 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);

        if (moveDirection.magnitude > 0f)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }
    private void FixedUpdate()
    {
        if (!canMove)
            return;

        RaycastHit2D hit = Physics2D.Raycast(feet.position, Vector2.right, feetMargin);
        if (hit && moveDirection.x > 0f)
            moveDirection.x = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.left, feetMargin);
        if (hit && moveDirection.x < 0f)
            moveDirection.x = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.down, feetMargin);
        if (hit && moveDirection.y < 0f)
            moveDirection.y = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.up, feetMargin);
        if (hit && moveDirection.y > 0f)
            moveDirection.y = 0f;

        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime;
    }
}
