public class TornadoCommandPress : I_Command
{
    public I_Reciever reciever { get; set; }

    public TornadoCommandPress(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_FlappersReciever).Tornado(TornadoPress.Press);
    }
}
/*----------------------------------------------------*/
public class TornadoCommandHold : I_Command 
{
    public I_Reciever reciever { get; set; }

    public TornadoCommandHold(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_FlappersReciever).Tornado(TornadoPress.Hold);
    }
}
/*----------------------------------------------------*/
public class TornadoCommandRelease : I_Command
{
    public I_Reciever reciever { get; set; }

    public TornadoCommandRelease(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_FlappersReciever).Tornado(TornadoPress.Release);
    }
}