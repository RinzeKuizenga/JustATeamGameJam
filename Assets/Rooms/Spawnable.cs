using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnable : MonoBehaviour
{
    public int spawnDialogId = 3;

    // Unused shit code kept for decorative reasons
    // The previous comment was written exactly at 03:00 AM out of sheer frustration and
    // is left as permanent evidence of my volatile stupidity
    public void CheckSpawn(Move move)
    {
        foreach (var currentId in move.seenDialogId)
            if (currentId == 3)
                gameObject.SetActive(true);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
