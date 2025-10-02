using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 500f;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public Animator animator;
    private bool isGrounded = false;
    private Rigidbody2D rg;
    private InputActions input = null;
    private Vector2 moveVector = new Vector2();
    private Vector3 ogScale = Vector3.zero;

    void Awake()
    {
        input = new InputActions();
        rg = GetComponent<Rigidbody2D>();
        ogScale = transform.localScale;
    }

    void FixedUpdate()
    {
        // Do ground checks
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);

        isGrounded = false;
        animator.SetBool("IsGrounded", false);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.layer != 6) // Player layer
                {
                    isGrounded = true;
                    animator.ResetTrigger("Jump");
                    animator.SetBool("IsGrounded", true);
                    break;
                }
            }
        }

        // Move
        rg.position += moveVector * speed * Time.deltaTime;

        animator.SetFloat("yVelocity", rg.linearVelocity.y);

        // Orient player
        if (moveVector.x > 0)
            transform.localScale = new Vector3(-ogScale.x, ogScale.y, ogScale.z);
        else if (moveVector.x < 0)
            transform.localScale = new Vector3(ogScale.x, ogScale.y, ogScale.z);

        if (moveVector.x != 0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }
    private void Update()
    {
        if (input.Player.Jump.triggered)
            Jump();
    }

    private void Jump()
    {
        if (!isGrounded)
            return;

        rg.AddForce(Vector2.up * jumpForce);
        animator.SetTrigger("Jump");
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
