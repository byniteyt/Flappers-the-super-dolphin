using UnityEngine;

public class Candado : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private float fuerza;
    private bool atraido;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void IrA(Vector3 pos, float force)
    {
        rb.isKinematic = false;
        dir = (pos - transform.position).normalized;
        fuerza = force;
        atraido = true;
    }

    private void FixedUpdate()
    {
        if (!atraido)
            return;

        rb.AddForce(dir * fuerza, ForceMode.Acceleration);
    }

    public void DetenerAtraccion()
    {
        atraido = false;
    }
}