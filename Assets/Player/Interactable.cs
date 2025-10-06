#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public Animator animator;

#if UNITY_EDITOR
    public SceneAsset sceneToLoad; // Only visible in Editor
#endif

    [SerializeField, HideInInspector]
    private string sceneToLoadName; // Used at runtime

    public GameObject ui;
    public int dialogId = 0;
    private Move player;
    private confirmBox confirmUI;
    private GameObject interactUI;
    private GameObject uiPrefab;

    public void Animate(string paramName) => animator.SetBool(paramName, true);
    public void UnAnimate(string paramName) => animator.SetBool(paramName, false);

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player"))
            return;

        player = c.GetComponent<Move>();
        interactUI = player.interactUI;
        confirmUI = player.confirmUI;
        interactUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (!c.CompareTag("Player"))
            return;

        player = null;
        if (interactUI)
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

            if (!string.IsNullOrEmpty(sceneToLoadName))
            {
                confirmUI.gameObject.SetActive(true);
                confirmUI.player = player;
                confirmUI.sceneToLoadName = sceneToLoadName;
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
            if (dt)
                dt.Begin(player);
        }
    }

    private void Start()
    {
        if (ui)
        {
            uiPrefab = Instantiate(ui);
            uiPrefab.SetActive(false);
        }

        if (confirmUI)
            confirmUI.gameObject.SetActive(false);

        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.name != "Canvas" || !uiPrefab)
                continue;

            uiPrefab.transform.SetParent(go.transform, false);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneToLoad != null)
            sceneToLoadName = sceneToLoad.name;
    }
#endif
}
