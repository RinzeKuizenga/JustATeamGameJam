using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnables;
    private Move player;

    public void Spawn()
    {
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!go.CompareTag("Player"))
                continue;

            player = go.GetComponent<Move>();
        }
        foreach (var spawnable in spawnables)
            if (player.seenDialogId.Contains(spawnable.GetComponent<Spawnable>().spawnDialogId))
            {
                var spawn = Instantiate(spawnable);
                spawn.transform.SetParent(transform, false);
            }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }
}
