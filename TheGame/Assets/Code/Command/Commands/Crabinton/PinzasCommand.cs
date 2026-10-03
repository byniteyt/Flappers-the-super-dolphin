public class PinzasCommand : I_Command
{
    public I_Reciever reciever { get; set; }
    bool active = false;

    public PinzasCommand(I_Reciever rec, bool active)  
    {
        this.reciever = rec;
        this.active = active;
    }

    public void Execute() 
    {
        (reciever as CrabintonReciever).Pinzas(active);
    }
}