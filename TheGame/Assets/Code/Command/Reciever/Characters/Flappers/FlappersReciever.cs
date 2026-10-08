using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class FlappersReciever : MonoBehaviour, I_FlappersReciever
{
    NormalFlapperController normalFlappers;  
    BuffFlappers buffFlappers;

    [SerializeField] bool buff;

    [Header("Referencias")]
    [SerializeField] Transform cameraTransform;

    [Header("Stats")]
    public float power;
    public float MaxPower;
    public float speed = 5f;

    [SerializeField] Slider slider;

    private void Awake()
    {

        normalFlappers = GetComponentInChildren<NormalFlapperController>();
        buffFlappers = GetComponentInChildren<BuffFlappers>();

        normalFlappers.gameObject.SetActive(true);
        buffFlappers.gameObject.SetActive(false);

        power = MaxPower;
        slider.maxValue = MaxPower;
    }

    public void Pause() 
    {
        
    }

    public void InitMove() 
    {

    }

    public void Move(Vector2 dir) 
    {
        
        if (buff)
        {
            buffFlappers.Move(dir);
            return;
        }
        normalFlappers.Move(dir);
    }

    public void Attack() 
    {
    
    }

    public void Dash() 
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(rigidBody.linearVelocity * 10, ForceMode.Impulse);
    }

    public void Buff() 
    {
        if (!buff && power >= buffFlappers.Cost) 
        {
            buff = true;
            power -= buffFlappers.Cost;
            StartCoroutine(FlapperTransition());
        }
    }

    public void GoNormal() 
    {
        StartCoroutine(FlapperInverseTransition());
    }

    IEnumerator FlapperTransition() 
    {
        yield return new WaitForSeconds(5);

        normalFlappers.gameObject.SetActive(false);
        buffFlappers.gameObject.SetActive(true);
    }

    IEnumerator FlapperInverseTransition() 
    {
        yield return new WaitForSeconds(5);

        normalFlappers.gameObject.SetActive(true);
        buffFlappers.gameObject.SetActive(false);
        buff = false;
    }

    public void Tornado(TornadoPress type) 
    {
        if (!buff) 
        {
            normalFlappers.Tornado(type);
        }
    }

    private void Update()
    {
        slider.value = power;
    }
}