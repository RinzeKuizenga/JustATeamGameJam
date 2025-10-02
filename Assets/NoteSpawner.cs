using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnNote();
            timer = 0f;
        }
    }

    void SpawnNote()
    {
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
        note.GetComponent<Note>().key = KeyCode.Z;
    }
}
