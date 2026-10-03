public class AttackCommand : I_Command
{
    public I_Reciever reciever { get; set; }

    public AttackCommand(I_Reciever reciever) 
    {
        this.reciever = reciever;
    }

    public void Execute() 
    {
        (reciever as I_CharacterReciever).Attack();
    }
}