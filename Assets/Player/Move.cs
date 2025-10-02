using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 2f;
    public Animator animator;
    public Transform feet;
    public float feetMargin = 0.2f;
    private Vector2 moveDirection = Vector2.zero;

    void Update()
    {
        moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            moveDirection += speed * Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveDirection += speed * Vector2.right;
        if (Input.GetKey(KeyCode.W))
            moveDirection += speed * Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection += speed * Vector2.down;

        if (moveDirection.x > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (moveDirection.x < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (moveDirection.magnitude > 0f)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.position, Vector2.right, feetMargin);
        if (hit && moveDirection.x > 0f)
            moveDirection.x = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.left, feetMargin);
        if (hit && moveDirection.x < 0f)
            moveDirection.x = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.down, feetMargin);
        if (hit && moveDirection.y < 0f)
            moveDirection.y = 0f;

        hit = Physics2D.Raycast(feet.position, Vector2.up, feetMargin);
        if (hit && moveDirection.y > 0f)
            moveDirection.y = 0f;

        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime;
    }
}
