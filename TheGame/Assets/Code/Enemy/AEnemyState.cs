using UnityEngine;

public abstract class AEnemyState : ScriptableObject, IEnemyState
{
    protected Rigidbody rigidbody;
    protected AEnemy body;
    protected Transform transform => body.transform;
    public AEnemy Body { get => body; set => body = value; }
    public virtual void Exit() {
        Debug.Log("Exit Enemy State: " + this.GetType().Name);
    }

    public virtual void FixedUpdate() { }

    public virtual void Init(AEnemy enemy)
    {
        body = enemy;
        rigidbody = body.GetComponent<Rigidbody>();
        Debug.Log("Body: " + body);
    }

    public virtual void Update() { }

    #region Collision and Trigger Events
    public virtual void OnCollisionEnter(Collision collision)
    {
        // Handle collision event
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        // Handle collision exit event
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        // Handle collision stay event
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        // Handle trigger enter event
    }

    public virtual void OnTriggerExit(Collider collision)
    {
        // Handle trigger exit event
    }

    public virtual void OnTriggerStay(Collider collision)
    {
        // Handle trigger stay event
    }
    #endregion
}
