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
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
        }
    }
}