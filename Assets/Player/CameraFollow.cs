using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float lerpSpeed = .2f;
    public float smoothTime = 0.3f;
    public int cameraDistance = -8;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        var target = player.transform.position + new Vector3(0, 1, cameraDistance);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
