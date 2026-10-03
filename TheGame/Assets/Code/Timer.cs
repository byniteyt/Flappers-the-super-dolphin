
using UnityEngine;

public class Timer
{
    float maxTime, time;

    public Timer(float maxTime)
    {
        this.maxTime = maxTime;
    }

    public void SetNewTime(float maxTime) 
    {
        this.maxTime = maxTime;
    }

    public bool isRunning() 
    {
        return time > 0;
    }

    public void StartTimer() 
    {
        time = maxTime;
    }

    public void UpdateTimer() 
    {
        if (isRunning()) 
        {
            time -= Time.deltaTime;
        }
    }
}
