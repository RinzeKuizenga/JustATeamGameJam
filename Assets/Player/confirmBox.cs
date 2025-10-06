#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class confirmBox : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset sceneToLoad; // Only editable in Editor
#endif

    [SerializeField, HideInInspector]
    public string sceneToLoadName; // Used at runtime

    public Move player;

    public void LoadS()
    {
        if (string.IsNullOrEmpty(sceneToLoadName))
            return;

        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
            go.SetActive(false);

        SceneManager.LoadScene(sceneToLoadName, LoadSceneMode.Additive);
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
        player.canMove = true;
        player.interactUI.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneToLoad != null)
            sceneToLoadName = sceneToLoad.name;
    }
#endif
}
