using UnityEngine;

public class LevelSelectionCamera : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform destination;

    private void Awake()
    {
        transform.position = destination.position;
    }

    public void StartMove(Transform pos) 
    {
        destination = pos;
    }
     
    private void FixedUpdate() 
    {
        if (/*Vector3.Distance(transform.position, destination.position) > speed - 0.1f*/ true)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }
    }
}