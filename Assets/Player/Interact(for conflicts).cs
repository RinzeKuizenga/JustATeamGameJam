using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Interact : MonoBehaviour
{
    /*
    public List<Interactable> interactables=new List<Interactable>();
    //can be changed to gameobjects too, just an extra step to convert them to transforms
    public float distanceToInteract = 10;
    // how far max to activate something
    public GameObject confirmation;
    public GameObject EToInteract; //Place EBox in this; EBox MUST be in a canva to work
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
                        confirmation.SetActive(true);
                    }
                    //animate with t.Animate()
                }
            }
            if (t.gameObject.activeInHierarchy)
            {
                e = true;
            }
        }
        EToInteract.SetActive(e);
        */
    }
}
