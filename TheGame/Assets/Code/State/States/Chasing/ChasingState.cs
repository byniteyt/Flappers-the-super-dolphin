using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace ChasingPJ
{
    public abstract class ChasingState : AEnemyState
    {
        protected Transform target;
        protected float delay;
        [SerializeField] protected float speed;
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
            target = GameObject.FindWithTag("Player").transform;
        }
        protected abstract IEnumerator WaitUntilAttack();
        public override void Update()
        {
            
        }
        
    }
}

