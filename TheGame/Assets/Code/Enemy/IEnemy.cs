using Unity.VisualScripting;
using UnityEngine;

public interface IEnemy 
{
    public GameObject GetPlayerOnSight();

    // Movement management
    public float GetRotateSpeed();
    public float GetWanderSpeed();
    public float GetChaseSpeed();
    public void SetCurrentSpeed(float speed);
    public void MoveTo(Transform target, float speed, float rotateSpeed);

    // Waypoints management
    public void MoveToDestination(Transform destination, float speed, float rotateSpeed);

    // Active State 
    public void ActiveIdelState();
    public void ActivePatrolState();
    public void ActiveChaseState();
    public void ActiveAttackState();
    public void ChangeState(AEnemyState state);
}
