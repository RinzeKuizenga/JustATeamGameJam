using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public Animator animator;
    public SceneAsset sceneToLoad;
    public GameObject ui;
    public int dialogId = 0;
    private Move player;
    private confirmBox confirmUI;
    private GameObject interactUI;

    private GameObject uiPrefab;

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

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.gameObject.CompareTag("Player"))
            return;

        player = c.gameObject.GetComponent<Move>();
        interactUI = player.interactUI;
        confirmUI = player.confirmUI;
        interactUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (!c.gameObject.CompareTag("Player"))
            return;

        player = null;

        if (!interactUI)
            return;

        interactUI.SetActive(false);
    }

    private void Update()
    {
        if (!player)
            return;

        if (Input.GetKeyDown(KeyCode.E) && player.canMove)
        {
            interactUI.SetActive(false);

            if (uiPrefab)
                uiPrefab.SetActive(true);

            if (sceneToLoad)
            {
                confirmUI.gameObject.SetActive(true);
                confirmUI.player = player;
                confirmUI.sceneToLoad = sceneToLoad;
            }

            player.canMove = false;

            if (dialogId != 0)
                player.seenDialogId.Add(dialogId);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            confirmUI.gameObject.SetActive(false);
            interactUI.SetActive(false);
            player.canMove = true;

            if (!uiPrefab)
                return;

            var dt = uiPrefab.GetComponent<DialogTrigger>();
            uiPrefab.SetActive(false);

            if (!dt)
                return;

            dt.Begin(player);
        }
    }

    public void Start()
    {
        if (ui)
        {
            uiPrefab = Instantiate(ui);
            uiPrefab.SetActive(false);
        }


        if (confirmUI)
            confirmUI.gameObject.SetActive(false);
        // Self assign to player
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.name != "Canvas" || !uiPrefab)
                continue;

            uiPrefab.transform.SetParent(go.transform, false);
        }
        //// Self assign to player
        //foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        //{
        //    var move = go.GetComponentInChildren<Move>();
        //    if (move)
        //        move.interactables.Add(this);

        //    if (!movePrefabToCanvas || go.name != "Canvas" || !prefab)
        //        continue;

        //    prefab.transform.SetParent(go.transform, false);
        //    prefab.SetActive(false);
        //    prefabToLoad = prefab;
        //}
    }
}
