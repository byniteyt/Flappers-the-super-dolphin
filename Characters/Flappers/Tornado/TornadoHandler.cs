using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TornadoHandler : MonoBehaviour
{
    [SerializeField] Tornado tornado;

    [SerializeField] InputActionReference torndoInputHold;  

    [SerializeField] int TornadoActivationCost;

    PowerCounter powerCounter;

    private void Awake()
    {
        powerCounter = GetComponent<PowerCounter>();
        tornado.DeActivate();
    }

    private void OnEnable()
    {
        torndoInputHold.action.performed += Hold;
        torndoInputHold.action.Enable();

        torndoInputHold.action.canceled += Release;
    }

    private void OnDisable() 
    {
        torndoInputHold.action.performed -= Hold;
        torndoInputHold.action.canceled -= Release;
    }

    public void Hold(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (powerCounter.TryConsume(TornadoActivationCost))
        {
            powerCounter.RegisterAbility(this);
            StartCoroutine(TakePower());
            tornado.Activate();
        }
    }

    public void Release(InputAction.CallbackContext context)
    {
        Release();
    }

    void Release()
    {
        print("Relese");

        StopAllCoroutines();
        powerCounter.UnregisterAbility(this);
        tornado.DeActivate();
    }

    IEnumerator TakePower() 
    {
        yield return new WaitForSeconds(1);

        if (powerCounter.TryConsume(2))
            StartCoroutine(TakePower());
        else Release();
    }
}