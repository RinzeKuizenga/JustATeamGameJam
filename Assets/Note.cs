using UnityEngine;

public class Note : MonoBehaviour
{
    public enum NoteKey { Z, X, C }
    public enum Direction { Left, Right }

    public NoteKey key;
    public Direction direction;

    [HideInInspector]
    public Transform hitPoint;

    public float speed = 5f; // units per second
    private bool isHit = false;

    void Update()
    {
        if (isHit || hitPoint == null) return;

        // Move towards hit point
        Vector3 target = hitPoint.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Check if missed
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            GameManager.Instance.NoteMissed(this);
            Destroy(gameObject);
        }
    }

    public void OnHit()
    {
        isHit = true;
        Destroy(gameObject);
    }
}
