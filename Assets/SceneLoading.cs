using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Animator animator;
    public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Start");
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Rooms");
    }

    public void UnloadScene()
    {
        foreach (var go in SceneManager.GetSceneByName("Rooms").GetRootGameObjects())
        {
            if (go.name == "confirmBox" || go.name == "EBox")
                continue;

            go.SetActive(true);
        }
            

        foreach (var go in SceneManager.GetSceneByName(sceneName).GetRootGameObjects())
            go.SetActive(false);

        SceneManager.UnloadScene(sceneName);
    }
}
