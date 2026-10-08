using UnityEngine;
namespace EnemyIdle
{
    [CreateAssetMenu(fileName = "SleepingIdle", 
        menuName = "EnemyStates/Idle/Sleeping")]
    public class SleepingIdle : IdleState
    {
        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            Debug.Log("Entering Spectating Idle State");
        }

        public override void Exit()
        {
            base.Exit();
        }

        #region Collision and Trigger Events
        public override void OnCollisionEnter(Collision collision)
        {
            // Handle collision event
        }

        public override void OnCollisionExit(Collision collision)
        {
            // Handle collision exit event
        }

        public override void OnCollisionStay(Collision collision)
        {
            // Handle collision stay event
        }

        public override void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player")&&body.GetPlayerOnSight()!=null)
            {
                body.GetComponent<Animator>().enabled = true;
                body.ActiveChaseState();
            }
        }

        public override void OnTriggerExit(Collider collision)
        {
            // Handle trigger exit event
        }

        public override void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player") && body.GetPlayerOnSight() != null)
            {
                body.ActiveChaseState();
            }
        }
        #endregion
    }
}

