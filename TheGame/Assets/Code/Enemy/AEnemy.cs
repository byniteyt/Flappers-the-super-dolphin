using ChasingPJ;
using EnemyIdle;
using EnemyPatrol;
using PJAttacked;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, IEnemy
{
    protected Vector3 directionalView; 
    [SerializeField]
    protected Transform[] targets;
    public Transform[] Targets { get => targets; set => targets = value; }
    public Vector3 DirectionalView { get => directionalView; set => directionalView = value; }

    [Header("-----States-----")]
    [SerializeField] protected IdleState _IdleState;
    [SerializeField] protected PatrolState _PatrolState;
    [SerializeField] protected ChasingState _PJChasingState;
    [SerializeField] protected AttackingState _AttackingState;
    public virtual void ActiveAttackState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ActiveChaseState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ActiveIdelState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ActivePatrolState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ChangeState(AEnemyState state)
    {
        throw new System.NotImplementedException();
    }

    public virtual float GetChaseSpeed()
    {
        throw new System.NotImplementedException();
    }

    public virtual Transform GetCurrentWayPoint()
    {
        throw new System.NotImplementedException();
    }

    public abstract GameObject GetPlayerOnSight();

    public abstract float GetRotateSpeed();

    public abstract float GetReach();

    public abstract Vector3 GetDirection();

    public abstract float GetWanderSpeed();

    public abstract void MoveTo(Transform target, float speed, float rotateSpeed);
    public abstract void MoveTo(Vector3 target, float speed, float rotateSpeed);

    public virtual void MoveToDestination(Transform destination, float speed, float rotateSpeed)
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetCurrentSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }
}
