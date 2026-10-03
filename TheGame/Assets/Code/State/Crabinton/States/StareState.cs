using UnityEngine;

namespace Crabinton
{
    public class StareState : I_State
    {
        public I_StateController controller { get; set; }

        public StareState (I_StateController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            (controller as CrabintonController).ActivatePinzasCommand(true);
            (controller as CrabintonController).InitStare();
        }

        public void Exit()
        {

        }

        public void Handle()
        {
            (controller as CrabintonController).Staring();
        }

        public void HandleTransitions()
        {
            //if ((controller as CrabintonController))

            if ((controller as CrabintonController).HasFinishedStaring()) 
            {
                controller.SetState(new MoveState(controller));
            }
        }
    }
}