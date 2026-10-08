using Unity.VisualScripting;
using UnityEngine;

namespace EnemyPatrol
{
    public abstract class PatrolState : AEnemyState
    {
        protected bool isMoving;
        public float waitTime;
        public float speed = 2f;
        public float patrolDetection = 5f;

        protected int index = 0;
        protected Vector3 currentTarget;

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            MoveToNextTarget();
        }

        public override void Update()
        {

        } 
        protected void RecalculateTarget()
        {
            index=(index + 1) % body.Targets.Length;
            transform.LookAt(body.Targets[index]);
            MoveToNextTarget(); 
        }
        protected void MoveToNextTarget()
        {
            if (body.Targets.Length == 0) return;
            currentTarget = body.Targets[index].position;
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime));
        }
        public override void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}

