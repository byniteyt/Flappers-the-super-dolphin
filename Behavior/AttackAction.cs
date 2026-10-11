using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[agent] attacks flappers", category: "Action", id: "b84e9557e60373d811ce67f9ea09555c")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Anguila> Agent;

    protected override Status OnStart()
    {
        Agent.Value.Attack();
        return Status.Success;
    }
}

