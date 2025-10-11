using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Buttons : MonoBehaviour
{
    public GameObject end1;
    public GameObject end2;

    public bool ended = false;

    private Move move;
    //OnClick01 is on the button Option_01
    public void OnClick01()
    {
        //Turn ending option 1 active
        //var a = Instantiate(end1);
        //end1.Begin(move);

        end1.SetActive(true);
        ended = true;
    }

    //OnClick01 is on the button Option_02
    public void OnClick02() 
    {
        //Turn ending optiom 2 active
        //var a = Instantiate(end2);
        //end2.Begin(move);
        end2.SetActive(true);
        ended = true;
    }

    private void Start()
    {
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.name == "Canvas")
                transform.SetParent(go.transform, false);

            if (!go.CompareTag("Player"))
                continue;

            move = go.GetComponent<Move>();
            move.canMove = false;
        }
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !ended)
            return;

        SceneManager.LoadScene("Credits");
    }
}
