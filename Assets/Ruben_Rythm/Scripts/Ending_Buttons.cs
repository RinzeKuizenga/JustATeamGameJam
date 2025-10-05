using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Buttons : MonoBehaviour
{
    public DialogTrigger end1;
    public DialogTrigger end2;
    //OnClick01 is on the button Option_01
    public void OnClick01()
    {
        //Turn ending option 1 active
        end1.gameObject.SetActive(true);   
        SetFalse();
    }

    //OnClick01 is on the button Option_02
    public void OnClick02() 
    {
        //Turn ending optiom 2 active
        end2.gameObject.SetActive(true);
        SetFalse();
    }

    private void SetFalse()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!go.CompareTag("Player"))
                continue;

            go.GetComponent<Move>().canMove = false;
        }
    }
}
