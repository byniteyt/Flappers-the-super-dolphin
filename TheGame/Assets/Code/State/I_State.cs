
public interface I_State 
{
    public I_StateController controller { get; set; }

    public void Enter();
    public void Exit(); 

    public void Handle();
    public void HandleTransitions();
}