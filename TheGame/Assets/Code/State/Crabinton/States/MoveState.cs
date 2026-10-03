namespace Crabinton
{
    public class MoveState : I_State
    {
        public I_StateController controller { get; set; }

        public MoveState(I_StateController cont) 
        {
            controller = cont;
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
            (controller as CrabintonController).CommandMove();
        }

        public void HandleTransitions()
        {
            if ((controller as CrabintonController).HasFinishedMoving()) 
            {
                controller.SetState(new MoveBackState(controller));
            }
        }
    }
}