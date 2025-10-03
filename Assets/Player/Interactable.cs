using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public Animator animator;
    // public Scene sceneToLoad / GameObject prefabToLoad;

    public void Animate(string paramName)
    // paramName : parameter of the animator to animate
    {
        animator.SetBool(paramName, true);
    }

    public void UnAnimate(string paramName)
    // paramName : parameter of the animator to un-animate
    {
        animator.SetBool(paramName, false);
    }

    public void Update()
    {
        
    }

    public void Start()
    {
        
    }
}
