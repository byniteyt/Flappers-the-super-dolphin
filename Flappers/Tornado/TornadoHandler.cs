using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TornadoHandler : MonoBehaviour
{
    [SerializeField] Tornado tornado;

    [SerializeField] InputActionReference torndoInputHold;  

    [SerializeField] int TornadoActivationCost;

    private void Awake()
    {
        tornado.DeActivate();
    }

    private void OnEnable()
    {
        torndoInputHold.action.performed += Hold;
        torndoInputHold.action.Enable();

        torndoInputHold.action.canceled += Release;
        torndoInputHold.action.Enable();
    }

    private void OnDisable() 
    {
        torndoInputHold.action.performed -= Hold;
        torndoInputHold.action.canceled -= Release;
    }

    public void Hold(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (PowerCounter.TryConsume(TornadoActivationCost))
        {
            PowerCounter.RegisterAbility(this);
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
        PowerCounter.UnregisterAbility(this);
        tornado.DeActivate();
    }

    IEnumerator TakePower() 
    {
        yield return new WaitForSeconds(1);

        if (PowerCounter.TryConsume(2))
            StartCoroutine(TakePower());
        else Release();
    }
}