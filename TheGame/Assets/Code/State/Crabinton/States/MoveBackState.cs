using Crabinton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackState : I_State
{
    public I_StateController controller { get; set; }

    public MoveBackState(I_StateController controller) 
    {
        this.controller = controller;
    }

    public void Enter()
    {
        (controller as CrabintonController).InitMoveCommand();
    }

    public void Exit()
    {

    }

    public void Handle()
    {
        (controller as CrabintonController).MoveBack();
    }

    public void HandleTransitions()
    {
        if ((controller as CrabintonController).IsInPosition()) 
        {
            controller.SetState(new BombState(controller));
        }
    }
}
