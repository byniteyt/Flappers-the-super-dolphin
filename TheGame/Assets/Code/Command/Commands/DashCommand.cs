public class DashCommand : I_Command
{
    public I_Reciever reciever { get; set; }

    public DashCommand(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute()
    {
        (reciever as I_CharacterReciever).Dash();
    }
}