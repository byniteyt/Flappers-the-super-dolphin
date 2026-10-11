using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;

    Vector2 movement;   // aunque sea juego 3D solo nos interesa el movimiento en el plano XZ, en el Y lo haremos diferente
    bool isDashing = false;

    [Header("Velocidades")]
    public float speed = 5f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference movementInput;
    [SerializeField] InputActionReference interactionInput;

    [Header("Guía")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        interactionInput.action.performed += Dash;
    }

    private void OnDisable()
    {
        interactionInput.action.performed -= Dash;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementInput.action.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        Vector3 direction = cameraTransform.right * movement.x + cameraTransform.forward * movement.y;

        direction.Normalize();

        rigidBody.linearVelocity = direction * speed;
    }
    private void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            isDashing = true;
            rigidBody.AddForce(rigidBody.linearVelocity.normalized * 10, ForceMode.Impulse);
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        isDashing = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isDashing)
            {
                Vector3 pushDir = (collision.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<EnemyStateManager>().OnHit(pushDir);
                if (collision.gameObject.TryGetComponent(out Rigidbody enemyBody))
                {
                    /*collision.gameObject.GetComponent<EnemyStateManager>().enabled = false; 
                    
                    enemyBody.AddForce(pushDir * 8f, ForceMode.VelocityChange);

                    //enemyBody.AddForce(-collision.transform.forward, ForceMode.Impulse);*/

                    
                }
                // Aquí puedes agregar la lógica para manejar la colisión con el enemigo mientras estás haciendo dash
                Debug.Log("Colisión con enemigo mientras haces dash");
            }
            else
            {
                // Aquí puedes agregar la lógica para manejar la colisión con el enemigo cuando no estás haciendo dash
                Debug.Log("Colisión con enemigo sin hacer dash");
            }
        }
    }
}
