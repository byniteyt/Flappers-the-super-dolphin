using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersSprintHandler : MonoBehaviour
{
    [SerializeField] float sprintSpeed;

    [SerializeField] InputActionReference sprintInput;

    FlappersMoveHandler moveHandler;
    PowerCounter powerCounter;

    private void Awake()
    {
        powerCounter = GetComponent<PowerCounter>();
        moveHandler = GetComponent<FlappersMoveHandler>();
    }

    private void OnEnable()
    {
        sprintInput.action.performed += Sprint;
        sprintInput.action.Enable();

        sprintInput.action.canceled += SprintCancelled;
    }

    private void OnDisable()
    {
        sprintInput.action.performed -= Sprint;
        sprintInput.action.canceled -= SprintCancelled;

        sprintInput.action.Disable();
    }

    public void Sprint(InputAction.CallbackContext inputAction)  
    {
        if (powerCounter.TryConsume(1))
        {
            powerCounter.RegisterAbility(this);
            StartCoroutine(TakePower());
            moveHandler.SetSpeed(sprintSpeed);
        }
    }

    public void SprintCancelled(InputAction.CallbackContext inputAction) 
    {
        Release();
    }

    void Release()
    {
        StopAllCoroutines();
        powerCounter.UnregisterAbility(this);
        moveHandler.NormalSpeed();
        return;
    }

    IEnumerator TakePower()
    {
        yield return new WaitForSeconds(1);

        if (powerCounter.TryConsume(2))
            StartCoroutine(TakePower());
        else Release();
    }
}