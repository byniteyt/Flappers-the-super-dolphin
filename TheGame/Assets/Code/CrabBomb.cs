using UnityEngine;

public class CrabBomb : MonoBehaviour
{
    Rigidbody rb;

    Timer explosionTimer;
    [SerializeField] float explosionTime;

    [SerializeField] float force;
    GameObject explotion;

    private void OnEnable() 
    {
        explosionTimer = new Timer(explosionTime);
        if (rb == null) rb = GetComponent<Rigidbody>();

        if (explotion == null) explotion = Instantiate(Resources.Load<GameObject>("ExplosionEffect2"));
    }

    public void Throw()
    {
        rb.linearVelocity = Vector3.zero;
        explotion.SetActive(false);
        explosionTimer.StartTimer();
        rb.AddForce(transform.forward.normalized * force, ForceMode.Impulse);
    }

    private void Update()
    {
        explosionTimer.UpdateTimer();

        if (!explosionTimer.isRunning())
        {
            Explode();
        }
    }

    void Explode() 
    {
        explotion.SetActive(true);
        explotion.transform.position = transform.position;
        explotion.GetComponent<csDestroyEffect>().Init();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject g = collision.gameObject;
        if (g.CompareTag("Player") || g.CompareTag("Layout")) 
        {
            Explode();
        }
    }
}