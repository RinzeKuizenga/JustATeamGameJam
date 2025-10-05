using UnityEngine;
using UnityEngine.SceneManagement;

public class ZFuck : MonoBehaviour
{
    private Move player;
    public float zMax = -2f;
    public float zMin = -0.5f;

    public float offset = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!go.CompareTag("Player"))
                continue;

            player = go.GetComponent<Move>();
        }
    }

    void FixedUpdate()
    {
        if (!player)
            return;

        if (player.feet.transform.position.y > transform.position.y + offset)
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
    }
}
