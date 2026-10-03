public class RomperEstalagtitasCommand : I_Command 
{
    public I_Reciever reciever { get; set; }

    public RomperEstalagtitasCommand(I_Reciever reciever)
    {
        this.reciever = reciever;
    }

    public void Execute() 
    {
        (reciever as PulpoReciever).RomperEstalagtitas();
    }
}