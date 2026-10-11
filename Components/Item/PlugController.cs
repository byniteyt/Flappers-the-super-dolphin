using System.Collections;
using UnityEngine;

public class PlugController : MonoBehaviour 
{
    [SerializeField] Transform fishPos;

    [SerializeField] Iman[] imanes;

    [SerializeField] bool final;

    public bool ocupado = false;

    public Transform GetFishPos() { return fishPos; }

    public void TurnOn() 
    {
        print("plug encendido");
        ocupado = true;
        if (!final) GetComponent<BoxCollider>().enabled = false;
        foreach (var iman in imanes) { iman.AddEnergy(); }
    }

    public void TurnOff() 
    {
        print("plug apagado");
        ocupado = false;
        StartCoroutine(WaitSec());
        foreach (var iman in imanes) { iman.TakeOutEnergy(); }
    }

    IEnumerator WaitSec() 
    {
        yield return new WaitForSeconds(4);
        GetComponent<BoxCollider>().enabled = true;
    }
}