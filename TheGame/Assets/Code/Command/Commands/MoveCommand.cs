
using UnityEngine;

public class MoveCommand : I_Command
{
    public I_Reciever reciever { get; set; }
    Vector2 dir;

    public MoveCommand(I_Reciever reciever, Vector2 dir) 
    {
        this.reciever = reciever;
        this.dir = dir;
    }

    public void Execute() 
    {
        (reciever as I_CharacterReciever)?.Move(dir);
    }
}

public class InitMoveCommand : I_Command
{
    public I_Reciever reciever { get; set; }

    public InitMoveCommand(I_Reciever reciever)
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_CharacterReciever)?.InitMove();
    }
}
