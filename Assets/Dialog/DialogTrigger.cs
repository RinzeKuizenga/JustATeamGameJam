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
    public string trigger;
    public Animator animator;

    public GameObject endBox;
    public GameObject monster;

    private DialogTalk d;

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
        else if (monster)
        {
            var animator = monster.GetComponent<Animator>();
            if (animator != null && trigger != string.Empty)
                animator.SetTrigger(trigger);
        }


        if (canvas == null)
        {
            canvas = Array.Find(SceneManager.GetActiveScene().GetRootGameObjects(), s => s.name == "Canvas").transform;
        }

        var instance = Instantiate(dialog, canvas);
        dialog.playerMoveComponent = move;

        if (monster)
            Instantiate(monster, spawner.transform);

        instance.filepath = textFilePath;
        instance.ending = endBox;
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

    //private void FixedUpdate()
    //{
    //    if (!d)
    //        return;

    //    if (!d.finished)
    //        return;

    //    Debug.Log(d.finished);

    //    if (endBoxAdded)
    //        return;

    //    if (endBox)
    //    {
    //        var e = Instantiate(endBox, canvas);
    //        e.SetActive(true);
    //        endBoxAdded = true;
    //    }   
    //}
}
