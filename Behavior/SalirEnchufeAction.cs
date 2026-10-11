using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SalirEnchufe", story: "[Pez] sale del enchufe", category: "Action", id: "046fcdd3d5b809326e8933cccc36b13a")]
public partial class SalirEnchufeAction : Action
{
    [SerializeReference] public BlackboardVariable<Anguila> Pez;

    protected override Status OnStart()
    {
        Pez.Value.SalirEnchufe();
        return Status.Success;
    }
}