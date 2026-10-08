using System.Collections;
using UnityEngine;

public enum TornadoState 
{
    active, coolDown 
}

public class TornadoHandler : MonoBehaviour
{
    Tornado tornado;
    FlappersReciever controller;

    [SerializeField] float TornadoActivationCost;  

    [SerializeField] bool pressing;
    [SerializeField] bool cont = false;

    private void Awake()
    {
        tornado = GetComponentInChildren<Tornado>();
        controller = GetComponentInParent<FlappersReciever>();

        Release();
    }

    public void Press() 
    {
        if (!cont && controller.power > TornadoActivationCost)
        {
            tornado.Activate();
            cont = true;
            controller.power -= TornadoActivationCost;
        }
    }

    public void Hold()
    {
        if (cont && !pressing) pressing = true;
    }

    public void Release()
    {
        cont = false;
        pressing = false;
        tornado.DeActivate();
    }

    private void Update()
    {
        PowerManagement();
    }

    void PowerManagement() 
    {
        if (pressing && controller.power > 0) 
        {
            controller.power -= Time.deltaTime;
            Debug.Log("Power: " + controller.power);

            if (controller.power <= 0) 
            {
                controller.power = 0;
                Release();
                return;
            }
            return;
        }
        
        if (!pressing && controller.power < controller.MaxPower) 
        {
            controller.power += Time.deltaTime;

            if (controller.power > controller.MaxPower)
            {
                controller.power = controller.MaxPower;
            }
            return;
        }
    }
}