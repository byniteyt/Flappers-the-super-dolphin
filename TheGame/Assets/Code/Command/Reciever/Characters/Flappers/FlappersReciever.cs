using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlappersReciever : MonoBehaviour, I_FlappersReciever
{
    NormalFlapperController normalFlappers;  
    BuffFlappers buffFlappers;

    [SerializeField] bool buff;

    public float power;
    public float MaxPower;
    public float speed = 5f;

    Rigidbody rigidBody;

    [SerializeField] Slider slider;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();

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
        rigidBody.linearVelocity = dir * speed;
    }

    public void Attack() 
    {
    
    }

    public void Dash() 
    {
        
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