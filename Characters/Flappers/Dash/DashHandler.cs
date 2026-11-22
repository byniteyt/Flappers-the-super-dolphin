using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DashHandler : MonoBehaviour
{
    #region Variables
     
    [Header("bools")]
    public static bool isDashing = false;
    public bool isCoolingDown = false;

    [Header("Times")]
    [SerializeField] float dashTime;
    [SerializeField] float coolDownTime;

    [Header("Components")]
    Rigidbody rb;

    [Header("Input Actions")]
    [SerializeField] InputActionReference interactionInput;

    [SerializeField] float dashForce;
    FlappersMoveHandler moveHandler;

    #endregion Variables

    #region Initial

    private void Awake()
    {
        moveHandler = GetComponent<FlappersMoveHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        interactionInput.action.performed += StartDash;
        interactionInput.action.Enable();
    }
    private void OnDisable()
    {
        interactionInput.action.performed -= StartDash;
    }

    #endregion Initial

    #region Dash

    void DashStart() 
    {
        GetComponent<CapsuleCollider>().enabled = false;
        rb.AddForce(moveHandler.GetDirection() * dashForce, ForceMode.Impulse);
        print("Dash Started");
    }

    void DashUpdate()
    {
        print("Dashing");
    }

    void DashEnd() 
    {
        GetComponent<CapsuleCollider>().enabled = true;
        print("Dash Ended");
    }

    #endregion Dash

    #region Process

    public void StartDash(InputAction.CallbackContext inputAction) 
    {
        if (isCoolingDown || isDashing) return;

        if (FlappersAttackHandler.isAttacking) return;

        if (inputAction.performed) 
        {
            isDashing = true;
            DashStart();
            StartCoroutine(Dash());
        }
    }

    void Update() 
    {
        if (isDashing) 
        {
            DashUpdate();
        }
    }

    IEnumerator Dash() 
    {
        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        isCoolingDown = true;
        DashEnd();

        StartCoroutine(DashCoolDown());
        yield return null;
    }

    IEnumerator DashCoolDown() 
    {
        yield return new WaitForSeconds(coolDownTime);

        isCoolingDown = false;
        yield return null;
    }

    #endregion Process
}