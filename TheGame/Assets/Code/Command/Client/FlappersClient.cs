using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersClient : MonoBehaviour
{

    FlappersInvoker invoker = new FlappersInvoker();
    I_FlappersReciever reciever;

    bool isDashing = false;
    Vector2 movement;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference simpleAttack;
    [SerializeField] private InputActionReference holdAttack;
    [SerializeField] private InputActionReference buff;
    [SerializeField] InputActionReference movementInput;
    [SerializeField] InputActionReference interactionInput;

    [Header("Referencias")]
    [SerializeField] Transform cameraTransform;

    void Start()
    {
        reciever = GetComponent<I_FlappersReciever>();
    }

    private void OnEnable()
    {
        simpleAttack.action.performed += ctx => invoker.Execute(new TornadoCommandPress(reciever));
        holdAttack.action.performed += ctx => invoker.Execute(new TornadoCommandHold(reciever));
        buff.action.performed += ctx => invoker.Execute(new BuffCommand(reciever));
        //interactionInput.action.performed += Dash;
    }

    private void OnDisable()
    {
        simpleAttack.action.performed -= ctx => invoker.Execute(new TornadoCommandPress(reciever));
        holdAttack.action.performed -= ctx => invoker.Execute(new TornadoCommandHold(reciever));
        buff.action.performed -= ctx => invoker.Execute(new BuffCommand(reciever));
        //interactionInput.action.performed -= Dash;
    }

    void Update()
    {
        movement = movementInput.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = cameraTransform.right * movement.x + cameraTransform.forward * movement.y;
        invoker.Execute(new MoveCommand(reciever,dir));
    }
    /*
    private void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            rigidBody.AddForce(rigidBody.linearVelocity * 10, ForceMode.Impulse);
        }
    }*/
}