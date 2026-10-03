using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crabinton
{
    public class BombState : I_State
    {
        public I_StateController controller { get; set; }

        public BombState(I_StateController controller) 
        {
            this.controller = controller;
        }

        public void Enter() 
        {
            (controller as CrabintonController).ThrowBomb();
        }

        public void Exit() 
        {
            
        }

        public void Handle() 
        {
            //(controller as CrabintonController).ThrowBomb();
        }

        public void HandleTransitions() 
        {
            controller.SetState(new MoveState(controller));
        }
    }
}
