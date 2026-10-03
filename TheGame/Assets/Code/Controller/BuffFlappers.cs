using UnityEngine;

public class BuffFlappers : MonoBehaviour
{
    Timer timer = new Timer(5);

    public int Cost;

    FlappersReciever reciever;

    private void Awake()
    {
        reciever = GetComponentInParent<FlappersReciever>();
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
}
