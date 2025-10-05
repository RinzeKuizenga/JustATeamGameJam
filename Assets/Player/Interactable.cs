using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public Animator animator;
    public SceneAsset sceneToLoad;
    public GameObject prefabToLoad;
    public bool movePrefabToCanvas = true;

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

    public void Start()
    {
        GameObject prefab = null;
        if (prefabToLoad)
            prefab = Instantiate(prefabToLoad);
        // Self assign to player
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var move = go.GetComponentInChildren<Move>();
            if (move)
                move.interactables.Add(this);

            if (!movePrefabToCanvas || go.name != "Canvas" || !prefab)
                continue;

            prefab.transform.SetParent(go.transform, false);
            prefab.SetActive(false);
            prefabToLoad = prefab;
        }
    }
}
