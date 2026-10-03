using UnityEngine;

public class MovableObjects : MonoBehaviour
{
    Rigidbody _rigidbody;
    bool inTornado;
    [SerializeField] float maxTornadoForce;
    [SerializeField] float forceSpeed;
    public float tornadoForce = 0;

    [SerializeField] float spinSpeed = 3;
    Vector3 spinUpDir;

    public int Damage;

    void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        tornadoForce = 0;
        Reset();
    }

    private void Update()
    {
        if (inTornado && tornadoForce < maxTornadoForce) 
        {
            tornadoForce += Time.deltaTime * forceSpeed;

            if (tornadoForce > maxTornadoForce)
                tornadoForce = maxTornadoForce;
        }
    }

    public void InTornado(Vector3 up)  
    {
        _rigidbody.isKinematic = true;
        tornadoForce = 0;
        inTornado = true;
        spinUpDir = up;
    }

    public void Reset()  
    {
        _rigidbody.isKinematic = false;
        _rigidbody.linearVelocity = Vector3.zero;
        tornadoForce = 0;
        inTornado = false;
    }

    public void Rotar(Vector3 pos) 
    {
        transform.RotateAround(pos, spinUpDir.normalized, spinSpeed);
    }

    public void Lanzar(Vector3 target) 
    {
        _rigidbody.isKinematic = false;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(target * tornadoForce, ForceMode.Impulse);
        inTornado = false;
    }
}
