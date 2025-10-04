using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogTalk dialog;
    public Transform canvas;
    public TextAsset textFile;
    public bool fired = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dialog == null || fired)
            return;

        if (!other.CompareTag("Player"))
            return;

        Instantiate(dialog, canvas);

        var move = other.GetComponent<Move>();
        if (!move)
            return;

        dialog.playerMoveComponent = move;

        dialog.file = textFile;
        dialog.Begin();
        fired = true;
    }
}
