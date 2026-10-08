using UnityEngine;

public class FlappersMoveHandler : MonoBehaviour, I_MoveHandler
{
    Rigidbody rigidbody;
    Transform camera;
    [SerializeField] float speed = 5f;

    void Awake()
    {
        camera = Camera.main.transform;
        rigidbody = GetComponentInParent<Rigidbody>();
    }
    public void InitMove()
    {
        //throw new System.NotImplementedException();
    }

    public void Move(Vector2 direction)
    {
        Vector3 dir = camera.right * direction.x + camera.forward * direction.y;

        rigidbody.linearVelocity = dir*speed*Time.deltaTime;
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }
}
