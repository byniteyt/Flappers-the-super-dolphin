public class FlappersInvoker : I_Invoker 
{
    public void Execute(I_Command com) 
    {
        com.Execute();
    }
}