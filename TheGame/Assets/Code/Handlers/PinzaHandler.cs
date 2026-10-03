using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PinzaHandler : MonoBehaviour
{
    Timer coolDownTimer;
    [SerializeField] float coolDown;

    [SerializeField] float attackDist;

    [SerializeField] bool active = false;

    Transform target;

    [SerializeField] Transform retraTarget;

    [SerializeField] float speed1, speed2;

    [SerializeField] Transform pos;
    [SerializeField] Transform calculosPos; 

    private void Awake()
    {
        coolDownTimer = new Timer(coolDown);
        active = true;

        target = FindFirstObjectByType<FlappersClient>().transform;
    }

    public void Activate() 
    {
        active = true;
    }

    public void Deactovate() 
    {
        active = false;
    }

    private void Update()
    {
        if (!active) return;

        coolDownTimer.UpdateTimer();

        if (coolDownTimer.isRunning()) return;

        if (Vector3.Distance(target.position, calculosPos.position) < attackDist) 
        {
            StartCoroutine(AttackPattern());
            coolDownTimer.StartTimer();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(calculosPos.position, attackDist);
    }

    IEnumerator AttackPattern() 
    {
        yield return StartCoroutine(Retraer());
        yield return StartCoroutine(Dirigir());
        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(VolverPos());

        yield return null;
    }

    IEnumerator Retraer() 
    {
        while (Vector3.Distance(transform.position, retraTarget.position) > 0.5f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, retraTarget.position, speed1 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Dirigir()  
    {
        Vector3 direccion = (target.position - pos.position).normalized;
        Vector3 punto = pos.position + direccion * attackDist;

        if (Vector3.Distance(pos.position, target.position) < attackDist) 
        {
            punto = target.position;
        }

        while (Vector3.Distance(transform.position, punto) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, punto, speed2 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator VolverPos() 
    {
        while (Vector3.Distance(transform.position, pos.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos.position, speed2 * Time.deltaTime);
            yield return null;
        }
    }
}