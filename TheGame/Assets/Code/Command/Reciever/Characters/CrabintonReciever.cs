using UnityEngine;

public class CrabintonReciever : MonoBehaviour, I_CharacterReciever
{
    Transform target;

    Animator animator;

    [SerializeField] PinzaHandler pinza1, pinza2;

    [SerializeField] Transform mainPoint;

    I_MoveHandler moveHandler;

    ThrowBombHandler throwBombHandler;

    [SerializeField] Transform child;

    Transform lastTarget;

    private void Awake()
    {
        target = FindFirstObjectByType<FlappersClient>().transform;
        lastTarget = target;

        throwBombHandler = GetComponent<ThrowBombHandler>();
        animator = GetComponent<Animator>();

        moveHandler = GetComponent<I_MoveHandler>();
    }

    public void Pause() 
    {
        animator.enabled = false;

        moveHandler.Pause();
    }

    public void InitMove() 
    {
        moveHandler.InitMove();

        animator.enabled = true;
        animator.SetBool("Move", true);

        MovimientoPatas[] pt = GetComponentsInChildren<MovimientoPatas>();
        foreach (var p in pt)
        {
            p.SetTarget(lastTarget == target ? mainPoint : target);
        }
    }

    public void Move(Vector2 dir) 
    {
        if (dir == Vector2.zero) 
        {
            (moveHandler as CrabintonMoveHandler).Move(true, new Vector2(target.position.x, target.position.z));
            lastTarget = target;
            
            return;
        }

        moveHandler.Move(new Vector2(mainPoint.position.x, mainPoint.position.z));

        lastTarget = mainPoint;
    }

    public void Attack() 
    {
        
    }

    public void Dash() 
    {
    
    }

    public void Pinzas(bool active) 
    {
        //pinza1.Activate();
        //pinza2.Activate();
    }

    public bool IsInPosition() 
    {

        //print(Vector3.Distance(transform.position, mainPoint.position));
        return Vector3.Distance(child.position, mainPoint.position) < (moveHandler as CrabintonMoveHandler).speed;
    }

    public void ThrowBombs()  
    {
        throwBombHandler.ThrowBombs();
    }
}