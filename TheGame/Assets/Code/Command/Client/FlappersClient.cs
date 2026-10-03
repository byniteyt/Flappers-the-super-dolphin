using UnityEngine;

public class FlappersClient : MonoBehaviour
{
    FlappersInvoker invoker = new FlappersInvoker();
    I_FlappersReciever reciever;

    void Start()
    {
        reciever = GetComponent<I_FlappersReciever>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            invoker.Execute(new TornadoCommandHold(reciever));
            //handler.Hold();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            invoker.Execute(new TornadoCommandPress(reciever));
            //handler.Press();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            invoker.Execute(new TornadoCommandRelease(reciever));
            //handler.Release();
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            invoker.Execute(new BuffCommand(reciever));
        }
    }
}