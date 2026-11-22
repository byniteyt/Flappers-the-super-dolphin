using UnityEngine;
using UnityEngine.InputSystem;

public class FlappersAttackHandler : MonoBehaviour
{
    public static bool isAttacking;

    [SerializeField] Transform attackDir;

    [SerializeField] private InputActionReference attackInput;

    [SerializeField] float attackRadius;

    Animator anim;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        if (!enabled) return;
        if (!inputAction.performed) return;

        anim.SetBool("Attack", true);
        isAttacking = true;

        Collider[] col = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (var c in col) 
        {
            AgentGeneric agent = c.GetComponent<AgentGeneric>();

            if (agent != null) 
            {
                Instantiate(Resources.Load<GameObject>("Hits/PunchVFX"), agent.transform.position, agent.transform.rotation);
                c.GetComponent<AgentGeneric>().GetKiked(DireccionDisparo(agent.transform)); 
            }

            Interactuable inter = c.GetComponent<Interactuable>();

            if (inter != null) 
            {
                c.GetComponent<Interactuable>().Interactuar();
            }    
        }
    }

    Vector3 DireccionDisparo(Transform agent)
    {
        return (attackDir.position - agent.position).normalized;
    }

    public void GetKiked(Vector3 dir)
    {
        rb.linearVelocity = dir;

        if (dir.sqrMagnitude > 0.001f)
            transform.forward = dir;

        rb.AddForce(dir.normalized, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}