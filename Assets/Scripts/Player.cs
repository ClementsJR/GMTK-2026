using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private bool acceptInput;
    private bool holdingPart;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private InteractableDetector detector;

    private Rigidbody rb;

    private bool jumpQueued;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        detector = GetComponentInChildren<InteractableDetector>();
        detector.RegisterPlayer(this);
    }

    private void Update()
    {
        if (!acceptInput)
            return;

        if (InputManager.Current.JumpPressed)
            jumpQueued = true;

        if (InputManager.Current.InteractPressed)
            Interact();
    }

    private void FixedUpdate()
    {
        if (!acceptInput)
            return;

        Move();
    }

    public void EnableMovement()
    {
        acceptInput = true;
    }

    public void DisableMovement()
    {
        acceptInput = false;
        rb.linearVelocity = Vector3.zero;
    }

    private void Move()
    {
        Vector2 input = InputManager.Current.Move;

        Vector3 desiredVelocity = new Vector3(input.x * moveSpeed, rb.linearVelocity.y, input.y * moveSpeed);

        rb.linearVelocity = desiredVelocity;

        if (jumpQueued && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        jumpQueued = false;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    public void Interact()
    {
    }
}