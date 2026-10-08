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


    void Start()
    {
        reciever = GetComponent<I_FlappersReciever>();
    }

    private void OnEnable()
    {
        simpleAttack.action.performed += ctx => invoker.Execute(new TornadoCommandPress(reciever));
        holdAttack.action.performed += ctx => invoker.Execute(new TornadoCommandHold(reciever));
        holdAttack.action.canceled += ctx => invoker.Execute(new TornadoCommandRelease(reciever));
        buff.action.performed += ctx => invoker.Execute(new BuffCommand(reciever));
        movementInput.action.canceled += ctx => invoker.Execute(new MoveCommand
            (reciever, Vector2.zero));
        movementInput.action.performed += ctx => invoker.Execute(new MoveCommand
            (reciever, movementInput.action.ReadValue<Vector2>()));
        //interactionInput.action.performed += ctx => invoker.Execute(new DashCommand(reciever));
        interactionInput.action.performed += ctx => Dash();
    }

    private void OnDisable()
    {
        simpleAttack.action.performed -= ctx => invoker.Execute(new TornadoCommandPress(reciever));
        holdAttack.action.performed -= ctx => invoker.Execute(new TornadoCommandHold(reciever));
        holdAttack.action.canceled -= ctx => invoker.Execute(new TornadoCommandRelease(reciever));
        buff.action.performed -= ctx => invoker.Execute(new BuffCommand(reciever));
        movementInput.action.performed -= ctx => invoker.Execute(new MoveCommand
            (reciever, movementInput.action.ReadValue<Vector2>()));
        movementInput.action.canceled -= ctx => invoker.Execute(new MoveCommand
            (reciever, Vector2.zero));
        interactionInput.action.performed -= ctx => Dash();
    }
    public void Dash()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(rigidBody.linearVelocity * 10, ForceMode.Impulse);
    }

}