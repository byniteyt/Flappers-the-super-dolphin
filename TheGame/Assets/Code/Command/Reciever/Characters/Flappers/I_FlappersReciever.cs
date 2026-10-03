public enum TornadoPress 
{
    Press, Hold, Release
}

public interface I_FlappersReciever : I_CharacterReciever
{
    public void Buff(); 

    public void Tornado(TornadoPress type);
}