using System;
using UnityEngine;

public class Iman : MonoBehaviour
{
    int plugsConnected;
    [SerializeField] int plugs;

    [SerializeField] float force;

    bool on = false;

    [SerializeField] float radio;

    public void AddEnergy() 
    {
        plugsConnected++;
        if (plugsConnected >= plugs) TurnOn();
    }

    public void TakeOutEnergy() 
    {
        plugsConnected--;
        if (plugsConnected < plugs) TurnOff();
    }

    public void TurnOn() 
    {
        on = true;
        print("Iman Encendido");
        GetComponent<winCond>().Win();
    }

    private void Update()  
    {
        if (!on) return;

        Collider[] coll = Physics.OverlapSphere(transform.position, radio);

        foreach (Collider c in coll)
        {
            Candado cand = c.GetComponent<Candado>();
            if (cand != null) 
            {
                cand.IrA(transform.position, force);
            }
        }
    }

    public void TurnOff() 
    {
        on = false;
        print("Iman Apagado");

        Collider[] coll = Physics.OverlapSphere(transform.position, radio);

        foreach (Collider c in coll)
        {
            Candado cand = c.GetComponent<Candado>();
            if (cand != null)
            {
                cand.DetenerAtraccion();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
