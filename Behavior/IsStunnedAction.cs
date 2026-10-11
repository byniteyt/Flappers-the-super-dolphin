using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "IsStunned", story: "agent is [stunned]", category: "Action", id: "3bc5a4af8b0ed991475268216215d299")]
public partial class IsStunnedAction : Action
{
    [SerializeReference] public BlackboardVariable<Anguila> Stunned;
    protected override Status OnStart()
    {
        return Stunned.Value.IsStunned() ? Status.Success : Status.Failure;
    }
}