using UnityEngine;

public class CrabintonMoveHandler : MonoBehaviour, I_MoveHandler
{
    [SerializeField] public float speed;

    [SerializeField] float MaxDist = 2f;

    [SerializeField] Transform child;

    public void InitMove()
    {

    }

    public void Move(Vector2 dir) 
    {
        child.position =
             Vector3.MoveTowards(child.position,
             new Vector3(dir.x, child.position.y, dir.y),
             speed * Time.deltaTime);
    }

    public void Move(bool dist, Vector2 dir)
    {
        if (!dist) 
        {
            Move(dir);
            return;
        }

        float distancia = Vector2.Distance(new Vector2(child.position.x, child.position.z), dir);

        if (distancia > MaxDist)
        {
            Move(dir);
        }
    }

    public void Pause() 
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MaxDist);
    }
}
