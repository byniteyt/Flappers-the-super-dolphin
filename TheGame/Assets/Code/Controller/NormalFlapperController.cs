using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalFlapperController : MonoBehaviour 
{
    TornadoHandler handler;
    I_MoveHandler moveHandler;

    private void Awake()
    {
        handler = GetComponent<TornadoHandler>();
        moveHandler = GetComponent<I_MoveHandler>();
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

    public void Move(Vector2 dir)
    {
        moveHandler.Move(dir);
    }
}