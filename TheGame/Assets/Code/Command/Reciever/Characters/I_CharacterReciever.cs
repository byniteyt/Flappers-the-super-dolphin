
using UnityEngine;

public interface I_CharacterReciever : I_Reciever
{
    public void InitMove(); 
    public void Move(Vector2 dir);

    public void Attack();

    public void Dash();
}