using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersAttackHandler : MonoBehaviour
{
    public static bool isAttacking;

    [SerializeField] InputActionReference attackInput;

    [SerializeField] float attackRadius;
    [SerializeField] float attackPower;

    Animator anim;

    [SerializeField] Transform dir;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        attackInput.action.performed += Attack;
        attackInput.action.Enable();
    }
    private void OnDisable()
    {
        attackInput.action.performed -= Attack;
    }

    public void Attack(InputAction.CallbackContext inputAction) 
    {
        if (!inputAction.performed) return;

        if (DashHandler.isDashing) return;

        anim.SetBool("Attack", true);
        isAttacking = true;

        Collider[] col = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (var c in col) 
        {
            if (c.gameObject.CompareTag("Enemy")) 
            {
                c.GetComponent<Anguila>().GetKiked(dir.forward, attackPower);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}