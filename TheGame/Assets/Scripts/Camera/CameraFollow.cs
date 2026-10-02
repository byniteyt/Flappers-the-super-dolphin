using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public float sensitivity = 0.1f;

    private float pitch = 0f;

    private void Start()
    {
        player = transform.parent;
    }
    void Update()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //Vector2 mouse = Mouse.current.delta.ReadValue();

        transform.Rotate(Vector3.up * mouse.x * sensitivity, Space.World);

        transform.Rotate( Vector3.right * -mouse.y * sensitivity, Space.Self);

        player.rotation = transform.rotation;
    }
}
