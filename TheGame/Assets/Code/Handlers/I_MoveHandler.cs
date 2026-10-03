using UnityEngine;

public interface I_MoveHandler
{
    public void InitMove();

    public void Move(Vector2 dir);

    public void Pause();
}
