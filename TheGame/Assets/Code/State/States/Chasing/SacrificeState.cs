using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace ChasingPJ
{
    [CreateAssetMenu(fileName = "SacrificeChasing",
        menuName = "EnemyStates/Chasing/Sacrifice")]
    public class SacrificeState : ChasingState
    {
        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            Debug.Log("Entering Sacrifice State");
        }
        public override void Update()
        {
            
        }
        public override void FixedUpdate()
        {
            if (target != null)
            {
                body.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                body.MoveTo(target, body.GetChaseSpeed(), body.GetRotateSpeed());
            }
            else
            {
                // Ignorar capa del body
                int maskToIgnore = 1 << body.gameObject.layer;
                int mask = ~maskToIgnore;

                // Dirección del body
                Vector2 dir = body.GetDirection().normalized;

                // Raycast
                RaycastHit2D hit = Physics2D.Raycast(
                    (Vector2)transform.position,
                    dir,
                    999f,
                    mask
                );

                if (hit.collider != null)
                {
                    Debug.Log("Impacto con " + hit.collider.name);
                    Debug.Log("Punto de impacto: " + hit.point);

                    // --- MOVER HACIA EL PUNTO DE COLISION ---
                    body.MoveTo(hit.point, body.GetChaseSpeed(), body.GetRotateSpeed());
                }

            }

            //body.gameObject.GetComponent<Rigidbody2D>().linearVelocity = body.GetDirection() * Time.deltaTime * 10;
        }
        public override void OnTriggerEnter(Collider collision)
        {
            target = body.GetPlayerOnSight()?.transform;
        }
        public override void OnTriggerStay(Collider collision)
        {
            target = body.GetPlayerOnSight()?.transform;
        }
        public override void OnTriggerExit(Collider collision)
        {
            target = null;
        }
        public override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //EventManager.Instance.HitPlayer?.Invoke(this,null);
            }
        }
        public override void OnCollisionStay(Collision collision)
        {
            //if (collision.gameObject.CompareTag("Player"))
            Debug.Log("El suicida se mantiene");
            Destroy(body.gameObject);
        }

        protected override IEnumerator WaitUntilAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}

