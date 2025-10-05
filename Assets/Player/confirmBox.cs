using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class confirmBox : MonoBehaviour
{
    public SceneAsset sceneToLoad=null;
    public Move player;

    public void LoadS() 
    {
        if (!sceneToLoad)
            return;

        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);

        // THEN UNLOAD THE SCENE AT SOME POINT
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
        player.canMove = true;
        player.interactUI.SetActive(true);
    }
}
