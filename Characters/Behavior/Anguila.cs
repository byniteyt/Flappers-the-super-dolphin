using Unity.Behavior;
using UnityEngine;

public class Anguila : ElectricAgents
{
    BlackboardVariable<bool> plugged; 

    PlugController plugController;

    public override void Awake() 
    {
        base.Awake();
        GetComponent<BehaviorGraphAgent>().BlackboardReference.GetVariable<bool>("plugged", out plugged);
    }

    /***************************************************************************************/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plug")) 
        {
            plugController = collision.gameObject.GetComponent<PlugController>();
            if (plugController.ocupado) return;
            plugController.TurnOn();

            capsuleCollider.enabled = false;
            rb.isKinematic = true;

            transform.position = plugController.transform.position;
            transform.forward = plugController.GetFishPos().forward;
            plugged.Value = true;
        }
    }

    public override void SalirEnchufe() 
    {
        transform.position = plugController.GetFishPos().position;
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        plugController.TurnOff();
    }
}