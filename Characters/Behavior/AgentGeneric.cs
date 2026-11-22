using Unity.Behavior;
using UnityEngine;

public abstract class AgentGeneric : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed;
    [SerializeField] float chaseSpeed;

    [SerializeField] int damage;
    [SerializeField] float stunnTime;

    [SerializeField] float kickedForce;

    int wayPointID;

    [SerializeField] float viewDistance;
    [SerializeField] float viewAngle;

    Transform Flappers;

    bool stunned;
    Timer timer = new Timer(0.02f);
    [SerializeField] float attackForce;

    //[SerializeReference]
    BlackboardVariable<bool> kicked;

    protected Rigidbody rb;
    protected CapsuleCollider capsuleCollider;

    public virtual void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        Flappers = FindFirstObjectByType<FlappertsWallet>().transform;

        GetComponent<BehaviorGraphAgent>().BlackboardReference.GetVariable<bool>("kicked", out kicked);
    }

    public virtual void Spawn(Vector3? pos) 
    {   
        if (pos != null && pos.Value != null) transform.position = pos.Value;
        rb.linearVelocity = Vector3.zero;
    }

    public virtual bool IsStunned() { return stunned; }
    public virtual bool IsInSight() 
    {
        Vector3 playerDirection = (Flappers.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, playerDirection);
        if (angle < viewAngle / 2)
        {
            if (Physics.Raycast(transform.position, playerDirection, out RaycastHit hit, viewDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return Vector3.Distance(transform.position, Flappers.position) < 0.5f;
    }

    bool CheckTimer()
    {
        timer.UpdateTimer();

        if (!timer.isRunning())
        {
            timer.StartTimer();
            return true;
        }

        return false;
    }

    public virtual void Roam() 
    {
        if (!enabled) return;
        if (!CheckTimer()) return;

        if (Vector3.Distance(transform.position, wayPoints[wayPointID].position) < 0.2f)
        {
            NextWyPoint();
            return;
        }
        transform.LookAt(wayPoints[wayPointID]);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        rb.linearVelocity = transform.forward * speed;
    }

    void NextWyPoint()
    {
        rb.linearVelocity = Vector3.zero;
        if (wayPointID < wayPoints.Length - 1)
        {
            wayPointID++;
        }
        else wayPointID = 0;
    }

    public virtual void Chase() 
    {
        if (!enabled) return;
        if (!CheckTimer()) return;

        transform.LookAt(Flappers);
        rb.AddForce(transform.forward * chaseSpeed, ForceMode.Impulse);
        rb.linearVelocity = transform.forward * chaseSpeed;
    }


    public virtual void Attack()
    {
        if (!enabled) return;
        Flappers.GetComponent<HealthSystem>().GetHit(damage);

        Vector3 dir = (Flappers.position - transform.position).normalized;

        //Flappers.GetComponent<FlappersAttackHandler>().GetKiked(dir * attackForce);
        rb.linearVelocity = Vector3.zero;

        //Flappers.GetComponent<FlappersMoveHandler>().GetStunned(stunnTime);   
    }

    public virtual void GetKiked(Vector3 dir)
    {
        transform.forward = rb.linearVelocity = dir;
        rb.AddForce(dir * kickedForce, ForceMode.Impulse);
        kicked.Value = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 origen = transform.position;
        Vector3 direccionJugador = Vector3.forward;

        if (Flappers != null)
            direccionJugador = (Flappers.transform.position - origen).normalized;

        // Dirección frontal y límites del cono de visión
        Vector3 forward = transform.forward;

        Vector3 limiteIzquierdo = Quaternion.Euler(0, -viewAngle / 2, 0) * forward;

        Vector3 limiteDerecho = Quaternion.Euler(0, viewAngle / 2, 0) * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origen, limiteIzquierdo * viewDistance);
        Gizmos.DrawRay(origen, limiteDerecho * viewDistance);

        // Alcance máximo
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(origen, viewDistance);

        // Dirección hacia el jugador
        if (Flappers != null)
        {
            float angle = Vector3.Angle(forward, direccionJugador);
            bool dentroDelAngulo = angle < viewAngle / 2;

            Gizmos.color = dentroDelAngulo ? Color.green : Color.red;
            Gizmos.DrawRay(origen, direccionJugador * viewDistance);
        }
    }
}
