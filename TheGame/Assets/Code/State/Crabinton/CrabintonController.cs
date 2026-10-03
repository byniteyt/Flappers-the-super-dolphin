using Crabinton;
using UnityEngine;

public class CrabintonController : MonoBehaviour, I_StateController, I_Client
{
    public I_State state { get; set; }

    public I_Reciever reciever { get; set; }
    public I_Invoker invoker { get; set; }

    Timer stareTimer;
    [SerializeField] Vector2 stareTime;

    Timer moveTimer; 
    [SerializeField] Vector2 moveTime; 

    private void Awake()
    {
        invoker = new BossInvoker();

        reciever = GetComponent<I_Reciever>();

        stareTimer = new Timer(0);
        moveTimer = new Timer(0);

        state = new StareState(this);
        state.Enter();
    }

    public void SetState(I_State state)
    {
        this.state.Exit();
            print(name + ": " + state);
        this.state = state;
        this.state.Enter();
    }

    private void Update()
    {
        state.Handle();
        state.HandleTransitions();
    }

    public void ActivatePinzasCommand(bool active)
    {
        invoker.Execute(new PinzasCommand(reciever, active));
    }

    /*--------------------------------*/

    public void InitStare() 
    {
        stareTimer.SetNewTime(Random.Range(stareTime.x, stareTime.y));
        stareTimer.StartTimer();
    }

    public void Staring() 
    {
        stareTimer.UpdateTimer();
    }

    public bool HasFinishedStaring() 
    {
        return !stareTimer.isRunning();
    }

    /*--------------------------------*/

    public void InitMoveCommand() 
    {
        invoker.Execute(new InitMoveCommand(reciever));
        moveTimer = new Timer(Random.Range(moveTime.x, moveTime.y));
        moveTimer.StartTimer();
    }

    public void CommandMove() 
    {
        invoker.Execute(new MoveCommand(reciever, Vector2.zero));
        moveTimer.UpdateTimer();
    }

    public bool HasFinishedMoving() 
    {
        return !moveTimer.isRunning();
    }

    public void MoveBack() 
    {
        invoker.Execute(new MoveCommand(reciever, Vector2.one));
    }

    public bool IsInPosition() 
    {
        return (reciever as CrabintonReciever).IsInPosition();
    }

    public void ThrowBomb() 
    {
        invoker.Execute(new BombCommand(reciever));
    }
}