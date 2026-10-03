using System.Collections.Generic;

public class BossInvoker : I_Invoker
{

    public List<I_Command> commands { get; set; }

    public void Execute(I_Command com) 
    {
        //commands.Add(com);
        com.Execute();
    }
}
