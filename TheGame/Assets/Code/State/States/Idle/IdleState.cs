using Unity.VisualScripting;
using UnityEngine;
namespace EnemyIdle
{
    
    public abstract class IdleState : AEnemyState
    {
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
        }

        public override void Update()
        {
            
        }
    }
}

