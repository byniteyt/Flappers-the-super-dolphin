using UnityEngine;

public interface IEnemyState 
{
    public void Init(AEnemy enemy);
    public void Exit();
    public void Update();
    public void FixedUpdate();

}
