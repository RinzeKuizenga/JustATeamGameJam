using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the singleton instance
    public static GameManager Instance { get; private set; }

    public Transform hitPoint;

    public float perfectDistance = 0.1f;
    public float greatDistance = 0.2f;
    public float goodDistance = 0.3f;

    private List<Note> activeNotes = new List<Note>();

    void Awake()
    {
        // Make sure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Register all notes in the scene
        Note[] notes = FindObjectsOfType<Note>();
        foreach (var note in notes)
        {
            note.hitPoint = hitPoint;
            activeNotes.Add(note);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) TryHit(Note.NoteKey.Z);
        if (Input.GetKeyDown(KeyCode.X)) TryHit(Note.NoteKey.X);
        if (Input.GetKeyDown(KeyCode.C)) TryHit(Note.NoteKey.C);
    }

    void TryHit(Note.NoteKey key)
    {
        if (activeNotes.Count == 0) return;

        Note bestNote = null;
        float closestDist = float.MaxValue;

        foreach (var note in activeNotes)
        {
            if (note.key != key) continue;

            float dist = Vector3.Distance(note.transform.position, hitPoint.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                bestNote = note;
            }
        }

        if (bestNote == null) return;

        // Judge based on distance
        if (closestDist <= perfectDistance) Debug.Log("Perfect!");
        else if (closestDist <= greatDistance) Debug.Log("Great!");
        else if (closestDist <= goodDistance) Debug.Log("Good!");
        else return;

        bestNote.OnHit();
        activeNotes.Remove(bestNote);
    }

    public void NoteMissed(Note note)
    {
        Debug.Log("Miss: " + note.key);
        if (activeNotes.Contains(note)) activeNotes.Remove(note);
    }
}
