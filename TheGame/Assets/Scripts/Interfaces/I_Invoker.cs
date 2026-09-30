
using System.Collections.Generic;

public interface I_Invoker 
{
    public List<I_Command> commands { get; set; }

    public void Execute(I_Command com);
}
