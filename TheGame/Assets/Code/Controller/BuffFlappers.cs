using UnityEngine;

public class BuffFlappers : MonoBehaviour
{
    Timer timer = new Timer(5);
    I_MoveHandler moveHandler;
    public int Cost;

    FlappersReciever reciever;

    private void Awake()
    {
        reciever = GetComponentInParent<FlappersReciever>();
        moveHandler = GetComponent<I_MoveHandler>();
    }

    private void OnEnable()
    {
        timer.StartTimer();
    }

    private void Update()
    {
        timer.UpdateTimer();

        if (!timer.isRunning()) 
        {
            reciever.GoNormal();
        }
    }

    public void Dash() 
    {

    }
    public void Move(Vector2 dir)
    {
        moveHandler.Move(dir);
    }
}
