using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    public DialogTalk dialog;
    public Transform canvas;
    public string textFilePath;
    public int id = 1;
    public int onlyAfterId = 0;
    public bool fired = false;

    public void Begin(Move move)
    {
        if (move.seenDialogId.Contains(id))
            return;
        move.seenDialogId.Add(id);

        if (onlyAfterId > 0)
            if (!move.seenDialogId.Contains(onlyAfterId))
                return;

        if (canvas == null)
        {
            canvas = Array.Find<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects(), s => s.name == "Canvas").transform;
        }

        var instance = Instantiate(dialog, canvas);
        dialog.playerMoveComponent = move;

        instance.filepath = textFilePath;
        instance.Begin();
        fired = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dialog == null || fired)
            return;

        if (!other.CompareTag("Player"))
            return;

        var move = other.GetComponent<Move>();
        if (!move)
            return;

        Begin(move);
    }
}
