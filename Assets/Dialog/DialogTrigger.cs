using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogTalk dialog;
    public TextAsset textFile;
    public bool fired = false;

    private void OnTriggerEnter(Collider other)
    {
        if (dialog == null || fired)
            return;

        Instantiate(dialog);
        dialog.file = textFile;
        dialog.Begin();
        fired = true;
    }
}
