using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersMoveHandler : MonoBehaviour
{
    Rigidbody rigidBody;

    [SerializeField] float baseSpeed;
    float speed;

    [SerializeField] InputActionReference movementInput;

    [Header("Guía")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform body;

    Vector3 direction;

    Vector2 movement;

    Animator animator;

    bool stunned = false;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        NormalSpeed();

        stunned = false;
    }

    void OnDisable()
    {
        direction = Vector3.zero;
        rigidBody.linearVelocity = Vector3.zero;
        animator.SetBool("Move", false);
    }

    public void NormalSpeed() 
    {
        speed = baseSpeed;
    }

    public void SetSpeed(float speed) 
    {
        this.speed = speed;
    }

    private void FixedUpdate()
    {
        if (!enabled) return;
        if (stunned) return;

        movement = movementInput.action.ReadValue<Vector2>();

        animator.SetBool("Move", movement != Vector2.zero);

        direction = cameraTransform.right * movement.x + cameraTransform.forward * movement.y;

        direction.Normalize();

        rigidBody.linearVelocity = direction * speed;
    }

    public Vector3 GetDirection() { return direction; }

    public void GetStunned(float time)
    {
        stunned = true;
        StartCoroutine(Stunned(time));
    }

    IEnumerator Stunned(float time) 
    {
        yield return new WaitForSeconds(time);
        stunned = false;
    }
}
