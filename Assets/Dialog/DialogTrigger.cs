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
    public Spawner spawner;
    public Animator animator;
    public string trigger;

    public GameObject endBox;
    public GameObject monster;

    private DialogTalk d;
    bool endBoxAdded = false;

    public void Begin(Move move)
    {
        if (move.seenDialogId.Contains(id))
            return;

        if (onlyAfterId > 0)
            if (!move.seenDialogId.Contains(onlyAfterId))
                return;

        move.seenDialogId.Add(id);

        if (animator != null && trigger != string.Empty)
            animator.SetTrigger(trigger);

        if (canvas == null)
        {
            canvas = Array.Find<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects(), s => s.name == "Canvas").transform;
        }

        var instance = Instantiate(dialog, canvas);
        dialog.playerMoveComponent = move;

        if (monster)
            Instantiate(monster, spawner.transform);

        instance.filepath = textFilePath;
        instance.Begin();
        d = instance;
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

        if (spawner)
            spawner.Spawn();
    }

    private void FixedUpdate()
    {
        if (!d)
            return;

        if (!d.finished)
            return;

        if (endBoxAdded)
            return;

        if (endBox)
        {
            Instantiate(endBox, canvas);
            endBox.SetActive(true);
            endBoxAdded = true;
        }   
    }
}
