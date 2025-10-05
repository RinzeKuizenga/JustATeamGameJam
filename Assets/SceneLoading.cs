using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Animator animator;

    [Header("Scene changers")]
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
        SceneManager.UnloadScene(sceneName);
    }
}
