using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Animator animator;
    public string sceneName;
    public AudioSource mainmenusound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainmenusound.Play();
            animator.SetTrigger("Start");
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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

            if (go.name == "Canvas")
            {
                go.transform.Find("ConfirmBox").GetComponent<confirmBox>().GoBack();
                go.transform.Find("EBox").gameObject.SetActive(false);
            }
        }
            
        foreach (var go in SceneManager.GetSceneByName(sceneName).GetRootGameObjects())
            go.SetActive(false);

        SceneManager.UnloadSceneAsync(sceneName);
    }
}
