
public interface I_Command 
{
    public I_Reciever reciever { get; set; }

    public void Execute();
}
