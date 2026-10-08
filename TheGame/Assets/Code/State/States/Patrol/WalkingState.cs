using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
namespace EnemyPatrol
{
    [CreateAssetMenu(fileName = "WalkingPatrol",
        menuName = "EnemyStates/Patrol/Walking")]
    public class WalkingState : PatrolState
    {
        public override void Exit()
        {
        }
        public override void FixedUpdate()
        {
            //if (!isMoving) return;
            Collider[] objs = Physics.OverlapSphere(transform.position, patrolDetection);
            bool playerDetected = false;
            foreach (var item in objs)
            {
                if (item.gameObject.CompareTag("Player"))
                {
                    playerDetected = true;
                    body.ActiveChaseState();
                }
            }
            Debug.Log("IsPlayer " + playerDetected);
            rigidbody.MovePosition(Vector3.MoveTowards(
                body.transform.position,
                currentTarget,
                speed * Time.fixedDeltaTime
            ));

            if ((body.transform.position - currentTarget).sqrMagnitude <= 0.05f * 0.05f)
            {
                isMoving = false;
                GlobalCoroutiner.Instance.RunCoroutine(WaitAndFlip());
            }
        }
        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            MoveToNextTarget();
            isMoving = true;
        }

        IEnumerator WaitAndFlip()
        {
            isMoving = false;
            yield return new WaitForSeconds(waitTime);

            RecalculateTarget();
            isMoving = true;
        }
        public override void Update()
        {
        }
        public override void OnCollisionEnter(Collision collision)
        {
                if (!(collision.collider.CompareTag("Untagged") || collision.collider.CompareTag("Suelo")))
                {
                    Debug.Log("Patrol Collision with " + collision.collider.name);
                    RecalculateTarget();
                    return;
                }
        }
    }
}
