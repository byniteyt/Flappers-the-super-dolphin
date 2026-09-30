using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    public float speed = 5f;
    Vector2 movement;   // aunque sea juego 3D solo nos interesa el movimiento en el plano XZ, en el Y lo haremos diferente
    bool isRotating = false;

    [Header("Input Actions")]
    [SerializeField] InputActionReference movementInput;
    [SerializeField] InputActionReference interactionInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        interactionInput.action.performed += RotatePlayer;
    }

    private void OnDisable()
    {
        interactionInput.action.performed -= RotatePlayer;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementInput.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //rigidBody.linearVelocity =Vector3.Cross(new Vector3(movement.x, 0, movement.y) * speed, transform.forward) ;
        rigidBody.linearVelocity = new Vector3(movement.x, 0, movement.y) * speed;
    }
    private void RotatePlayer(InputAction.CallbackContext context)
    {
        if (context.performed && !isRotating)
        {
            StartCoroutine(Rotate360());
        }
    }

    private IEnumerator Rotate360()
    {
        isRotating = true;

        float elapsed = 0f;
        float duration = 0.1f;
        float totalRotation = 360f;
        rigidBody.AddForce(movement * 10 * speed, ForceMode.Impulse);
        Vector2 dir = movement;
        while (elapsed < duration)
        {
            float deltaTime = Mathf.Min(Time.deltaTime, duration - elapsed);
            float rotationThisFrame = totalRotation / duration * deltaTime;

            transform.Rotate(rotationThisFrame * dir.y, 0f, rotationThisFrame * -dir.x);

            elapsed += deltaTime;

            yield return null;
        }

        isRotating = false;
    }
}
