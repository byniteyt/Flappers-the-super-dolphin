using System.Collections;
using UnityEngine;
namespace ChasingPJ
{
    [CreateAssetMenu(fileName = "BouncerChase",
        menuName = "EnemyStates/Chasing/Bouncer")]

    public class BouncerChase : ChasingState
    {
        [SerializeField] float bounceForce = 5f;
        bool isOnAir = false;
        Vector3 direction;
        protected override IEnumerator WaitUntilAttack()
        {
            yield return new WaitForSeconds(delay);
            body.GetComponent<Rigidbody2D>().AddForce(new Vector2(bounceForce / 3, bounceForce) * new Vector2(direction.x, body.DirectionalView.y), ForceMode2D.Impulse);
        }
        public override void Exit()
        {
            base.Exit();
        }

        public override void FixedUpdate()
        {

        }

        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            direction = (body.GetPlayerOnSight().transform.position - body.transform.position).normalized;
            isOnAir = false;
        }
        public override void Update()
        {
            if (isOnAir) // Esperamos a que el enemigo toque el suelo para volver a moverse
            {
                return;
            }
            GlobalCoroutiner.Instance.RunCoroutine(WaitUntilAttack());

        }
        public override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                if (!body.GetPlayerOnSight())
                {
                    body.DirectionalView *= new Vector2(-1, 1);
                    body.transform.GetComponent<SpriteRenderer>().flipX = !body.transform.GetComponent<SpriteRenderer>().flipX; // damos la vuelta por si el jugador pasa encima
                    if (!body.GetPlayerOnSight())
                    { // si aun asi el jugador no esta a la vista, volvemos al patrullaje
                        body.ActivePatrolState();
                        return;
                    }
                }
                direction = (body.GetPlayerOnSight().transform.position - body.transform.position).normalized;
                isOnAir = false;
            }
            if (collision.gameObject.CompareTag("Enemigo"))
            {
                Vector3 bestDirection = (transform.position - collision.transform.position).normalized;
                body.GetComponent<Rigidbody2D>().AddForce(bestDirection * bounceForce, ForceMode2D.Impulse);
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
            GlobalCoroutiner.Instance.StopCoroutine(this.WaitUntilAttack());
        }
    }
}