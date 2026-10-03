using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalFlapperController : MonoBehaviour 
{
    TornadoHandler handler;

    private void Awake()
    {
        handler = GetComponent<TornadoHandler>();
    }

    public void Tornado(TornadoPress type)
    {
        switch (type) {
            case TornadoPress.Press:
                handler.Press();
                break;

            case TornadoPress.Hold:
                handler.Hold();
                break;

            case TornadoPress.Release:
                handler.Release();
                break;

            default: break;
        }
    } 
}