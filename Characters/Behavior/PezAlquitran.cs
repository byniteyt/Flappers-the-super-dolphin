using Unity.Behavior;
using UnityEngine;

public class PezAlquitran : AgentGeneric
{
    Explosivo explosion;

    public override void Awake()
    {
        base.Awake();
        explosion = GetComponent<Explosivo>();
    }

    private void Start()
    {
        explosion.Respawn();
    }

    public override void Spawn(Vector3? pos) 
    {
        print("spawn " + name);
        base.Spawn(pos);
        explosion.Respawn();
        explosion.ResetExplotion();
    }
}