using Unity.Behavior;
using UnityEngine;

public class Anguila : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed;
    [SerializeField] float chaseSpeed;

    [SerializeField] uint damage;

    int wayPointID;

    [SerializeField] float viewDistance;
    [SerializeField] float viewAngle;

    Transform Flappers;

    bool stunned;
    Timer timer = new Timer(0.02f);
    [SerializeField] float attackForce;

    //[SerializeReference]
    BlackboardVariable<bool> kicked;
    BlackboardVariable<bool> plugged; 

    Rigidbody rb;
    CapsuleCollider capsuleCollider;

    PlugController plugController;

    private void Awake() 
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        Flappers = FindFirstObjectByType<FlappertsStats>().transform; 

        GetComponent<BehaviorGraphAgent>().BlackboardReference.GetVariable<bool>("kicked", out kicked);
        GetComponent<BehaviorGraphAgent>().BlackboardReference.GetVariable<bool>("plugged", out plugged);
    }

    public bool IsStunned() { return stunned; }

    public bool IsInSight() 
    {
        Vector3 playerDirection = (Flappers.transform.position - transform.position).normalized;
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
        return false;
    }

    /***************************************************************************************/

    public void Roam() 
    {
        if (!CheckTimer()) return;

        if (Vector3.Distance(transform.position, wayPoints[wayPointID].position) < 0.2f) 
        {
            NextWyPoint();
            return;
        }
        transform.LookAt(wayPoints[wayPointID]);
        //transform.position += transform.forward * speed;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        rb.linearVelocity = transform.forward * speed;
    }

    void NextWyPoint() 
    {
        rb.linearVelocity = Vector3.zero;
        if (wayPointID < wayPoints.Length -1)
        {
            wayPointID++;
        }
        else wayPointID = 0;
    }

    public void Chase() 
    {
        if (!CheckTimer()) return;

        transform.LookAt(Flappers);
        //transform.position += transform.forward * chaseSpeed;
        rb.AddForce(transform.forward * chaseSpeed, ForceMode.Impulse);
        rb.linearVelocity = transform.forward * chaseSpeed;
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

    public void GetKiked(Vector3 dir, float power) 
    {
        transform.forward = dir;
        rb.linearVelocity = dir.normalized * power;
        rb.AddForce(dir.normalized * power, ForceMode.Impulse);
        kicked.Value = true;
    }

    public void Attack() 
    {
        Flappers.GetComponent<FlappertsStats>().GetHit(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plug")) 
        {
            plugController = collision.gameObject.GetComponent<PlugController>();
            if (plugController.ocupado) return;
            plugController.TurnOn();

            capsuleCollider.enabled = false;
            rb.isKinematic = true;

            transform.position = plugController.transform.position;
            transform.forward = plugController.GetFishPos().forward;
            plugged.Value = true;
        }
    }

    public void SalirEnchufe() 
    {
        transform.position = plugController.GetFishPos().position;
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        plugController.TurnOff();
    }

    /***************************************************************************************/

    private void OnDrawGizmos()
    {
        Vector3 origen = transform.position;
        Vector3 direccionJugador = Vector3.forward;

        if (Flappers != null)
            direccionJugador = (Flappers.transform.position - origen).normalized;

        // Dirección frontal y límites del cono de visión
        Vector3 forward = transform.forward;

        Vector3 limiteIzquierdo =
            Quaternion.Euler(0, -viewAngle / 2, 0) * forward;

        Vector3 limiteDerecho =
            Quaternion.Euler(0, viewAngle / 2, 0) * forward;

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