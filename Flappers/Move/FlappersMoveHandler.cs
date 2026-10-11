using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersMoveHandler : MonoBehaviour
{
    Rigidbody rigidBody;

    public float speed = 5f;

    [SerializeField] InputActionReference movementInput;

    [Header("Guía")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform body;

    Vector3 direction;

    Vector2 movement;

    Animator animator;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        movement = movementInput.action.ReadValue<Vector2>();

        animator.SetBool("Move", movement != Vector2.zero);

        direction = cameraTransform.right * movement.x + cameraTransform.forward * movement.y;

        direction.Normalize();

        rigidBody.linearVelocity = direction * speed;
    }

    public Vector3 GetDirection() { return direction; }
}
