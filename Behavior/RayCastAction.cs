using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RayCast", story: "Check if [detector] has a target", category: "Action", id: "beadcb00ca64715568215f6fd4c7f102")]
public partial class RayCastAction : Action
{
    [SerializeReference] public BlackboardVariable<Anguila> Detector;

    protected override Status OnStart()
    {
        return Detector.Value.IsInSight() ? Status.Success : Status.Failure;
    }
}