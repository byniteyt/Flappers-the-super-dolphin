using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyPatrol
{
    [CreateAssetMenu(fileName = "BouncerPatrol",
        menuName = "EnemyStates/Patrol/Bouncer")]
    public class BouncerPatrol : PatrolState
    {
        bool isOnAir = false;
        [SerializeField] float bounceForce = 10f;
        public override void Exit()
        {
            // Implementation for exiting the BouncerPatrol state
        }
        public override void FixedUpdate()
        {
            // Implementation for fixed update logic in the BouncerPatrol state
        }
        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            if (body.DirectionalView.y==0)
            {
                body.DirectionalView = new Vector2(body.DirectionalView.x, 0.5f);
            }
            isOnAir = false;
            isMoving = false;
        }
        public override void Update()
        {
            if (isOnAir||isMoving) // Esperamos a que el enemigo toque el suelo para volver a moverse
            {
                return;
            }
            if(Mathf.Abs(transform.position.x-currentTarget.x)<=0.2f)
            {
                RecalculateTarget();
            }
            GlobalCoroutiner.Instance.RunCoroutine(BounceAndMove());
        }
        IEnumerator BounceAndMove()
        {
            isMoving = true;
            body.GetComponent<Rigidbody2D>().AddForce(new Vector2(bounceForce / 3, bounceForce*2) * body.DirectionalView, ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(waitTime);
            isMoving = false;
        }
        public override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                if (body.GetPlayerOnSight())
                {
                    body.ActiveChaseState();
                }
                isOnAir = false;
                isMoving = false;
            }
            
        }
        public override void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                isOnAir = true;
            }
        }
        private void OnDestroy()
        {
            GlobalCoroutiner.Instance.StopCoroutine(this.BounceAndMove());
        }
    }
}