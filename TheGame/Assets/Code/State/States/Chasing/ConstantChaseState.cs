using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
namespace ChasingPJ
{
    [CreateAssetMenu(fileName = "ConstantChase",
        menuName = "EnemyStates/Chasing/Constant")]
    public class ConstantChaseState : ChasingState
    {
        protected override IEnumerator WaitUntilAttack()
        {
            throw new System.NotImplementedException();
        }
        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            Debug.Log("Entering Constant Chase State");
        }
        public override void Update()
        {
            base.Update();
            // Implement constant chasing behavior here
            transform.LookAt(target);
            rigidbody.linearVelocity = (target.position - transform.position).normalized * speed;
        }
        public override void OnHit(Vector3 hitDirection)
        {
            GlobalCoroutiner.Instance.StartCoroutine(DisableState(1.5f, hitDirection));
        }

        IEnumerator DisableState(float time, Vector3 hitDirection)
        {
            transform.GetComponent<EnemyStateManager>().enabled = false;
            Debug.Log("Disabling EnemyStateManager after hit");
            rigidbody.AddForce(hitDirection * 8f, ForceMode.VelocityChange);
            yield return new WaitForSeconds(time);
            rigidbody.linearVelocity = Vector3.zero;
            transform.GetComponent<EnemyStateManager>().enabled = true;
            Debug.Log("Re-enabling EnemyStateManager after hit");
        }
    }
}