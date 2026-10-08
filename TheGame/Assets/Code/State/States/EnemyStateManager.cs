using System;
using UnityEngine;

public class EnemyStateManager : AEnemy
{
    [Serializable] enum Direction
    {
        Forward,
        Back,
        Left,
        Right
    }
    private AEnemyState currentState;
    [Header("-----Alcances-----")]
    [SerializeField, Tooltip("La distancia de la detección del enemigo")]
    private float detectionRange = 10f;
    [SerializeField, Tooltip("El alcance de ataque del enemigo")]
    private float attackRange = 2f;

    [Header("-----Velocidades-----")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 5f;

    [Header("-----Vista-----")]
    [SerializeField] float viewAngle = 90f;
    [SerializeField] Direction direction;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (direction)
        {
            case Direction.Forward:
                directionalView = transform.up; 
                break;
            case Direction.Right:
                directionalView = transform.right;
                break;
            case Direction.Back:
                directionalView = -transform.up;
                break;
            case Direction.Left:
                directionalView = -transform.right;
                break;
        }
        _IdleState = (_IdleState!=null)? Instantiate(_IdleState): null;
        _PatrolState = (_PatrolState!=null)? Instantiate(_PatrolState): null;
        _PJChasingState = (_PJChasingState!=null)? Instantiate(_PJChasingState): null;
        _AttackingState = (_AttackingState!=null)? Instantiate(_AttackingState): null;
        this.currentState = this._IdleState;
        currentState?.Init(this.gameObject.GetComponent<AEnemy>());
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Update();
    }
    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }


    #region Collider Events
    private void OnTriggerEnter(Collider collision)
    {
        currentState.OnTriggerEnter(collision);

    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
        
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Vector3 bestDirection = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(bestDirection * 2, ForceMode2D.Force);
        }
        currentState.OnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        currentState.OnCollisionStay(collision);
    }
    #endregion

    #region Getters and Setters
    public override GameObject GetPlayerOnSight()
    {
        GameObject player = GameObject.Find("Player");
        
        Vector2 playerDirection = (player.transform.position - this.transform.position).normalized;
        float angle = Vector2.Angle(directionalView, playerDirection);
        if (angle < viewAngle / 2)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, playerDirection,detectionRange);
            int i = 0;
            foreach (RaycastHit2D ray in hit)
            {
                //Debug.Log($"Colisión {i} es {hit[i].collider.name}");
                
                if (hit[i].collider.gameObject.name == "Player")
                {
                    return hit[i].collider.gameObject;
                }
                i++;
            }
        }

        return null;
    }
    
    public override float GetWanderSpeed()
    {
        throw new System.NotImplementedException();
    }

    public override float GetReach()
    {
        return detectionRange;
    }
    public override float GetChaseSpeed()
    {
        return moveSpeed;
    }

    public override void SetCurrentSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public override float GetRotateSpeed()
    {
        return rotSpeed;
    }

    public override Vector3 GetDirection()
    {
        return directionalView;
    }

    public override Transform GetCurrentWayPoint()
    {
        throw new System.NotImplementedException();
    }


    #endregion

    #region Movement Management
    public override void MoveTo(Transform target, float speed, float rotateSpeed)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
        toTargetRotation.x = 0f;
        toTargetRotation.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
            -rotateSpeed * Time.deltaTime);
        
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    public override void MoveTo(Vector3 target, float speed, float rotateSpeed)
    {
        Vector2 direction = (target - transform.position).normalized;
        Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
        toTargetRotation.x = 0f;
        toTargetRotation.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
            -rotateSpeed * Time.deltaTime);

        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
    public override void MoveToDestination(Transform destination, float speed, float rotateSpeed)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Active States
    public override void ChangeState(AEnemyState state)
    {
        if (state == null) // si el enemigo no tiene ese estado, permanece en el actual
            return;
        Debug.Log($"Cambiando de estado {currentState} a {state}");
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Init(gameObject.GetComponent<AEnemy>());
    }
    public override void ActiveIdelState()
    {
        ChangeState(this._IdleState);
    }

    public override void ActivePatrolState()
    {
        ChangeState(this._PatrolState);
    }

    public override void ActiveChaseState()
    {
        ChangeState(this._PJChasingState);
    }

    public override void ActiveAttackState()
    {
        ChangeState(this._AttackingState);
    }
    #endregion
    private void OnDestroy()
    {
        if (currentState != null)
            currentState.Exit();

        currentState = null;
    }

}
