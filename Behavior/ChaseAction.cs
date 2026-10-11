using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[agent] chases [or] moves", category: "Action", id: "feb1b2da6079ab96ff357ec7f3e95f17")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<Anguila> Agent;
    [SerializeReference] public BlackboardVariable<bool> chases;
    protected override Status OnStart()
    {
        if (chases) Agent.Value.Chase();
        else Agent.Value.Roam();
        return Status.Success;
    }
}

