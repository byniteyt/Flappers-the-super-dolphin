using UnityEngine;
namespace EnemyIdle
{
    [CreateAssetMenu(fileName = "StartWalking",
        menuName = "EnemyStates/Idle/Walk")]
    public class StartWalking : IdleState // Esta clase representa el estado en el que el enemigo comienza a caminar directamente
    {
        public override void Init(AEnemy enemy)
        {
            base.Init(enemy);
            enemy.ActivePatrolState();
        }
        public override void Update()
        {
        }
    }
}