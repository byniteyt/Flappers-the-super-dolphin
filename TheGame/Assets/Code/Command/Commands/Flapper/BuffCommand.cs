public class BuffCommand : I_Command
{
    public I_Reciever reciever { get; set; }

    public BuffCommand(I_Reciever reciever)
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_FlappersReciever).Buff();
    }
}