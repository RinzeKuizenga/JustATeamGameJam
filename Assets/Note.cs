using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode key = KeyCode.Z;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }
}
