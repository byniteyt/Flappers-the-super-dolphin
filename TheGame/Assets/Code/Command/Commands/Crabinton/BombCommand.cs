using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCommand : I_Command
{
    public I_Reciever reciever { get; set; }

    public BombCommand(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute() 
    {
        (reciever as CrabintonReciever).ThrowBombs();
    }
}