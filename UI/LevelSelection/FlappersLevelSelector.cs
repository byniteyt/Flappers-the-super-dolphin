using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlappersLevelSelector : MonoBehaviour
{
    [SerializeField] LevelRep prevLevel,currentLevel;
     
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference playAction; 

    SceneManagerUI sceneManagerUI;

    private Vector2Int movement;

    NavMeshAgent agent;

    private void Start()
    {
        sceneManagerUI = GetComponent<SceneManagerUI>();
        agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(currentLevel.transform.position.x, transform.position.y, currentLevel.transform.position.z);

        SelectLevel();
    }

    private void OnEnable()
    {
        movementInput.action.performed += OnMovement;
        movementInput.action.canceled += OnMovement;

        movementInput.action.Enable();

        playAction.action.performed += OnEnterLevel;
        playAction.action.Enable();
    }

    private void OnDisable()
    {
        movementInput.action.performed -= OnMovement;
        movementInput.action.canceled -= OnMovement;

        //movementInput.action.Disable();

        playAction.action.performed -= OnEnterLevel;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        movement = new Vector2Int(
            Mathf.RoundToInt(input.x),
            Mathf.RoundToInt(input.y)
        );

        Direction dir = GetMovementDir(); 

        // move
        foreach (var p in currentLevel.levels) 
        {
            if (p.dir == dir)
            {
                prevLevel = currentLevel;
                currentLevel = p.level;

                SelectLevel();

                return;
            }
        }
    }

    void SelectLevel() 
    {
        prevLevel.transform.localScale = new Vector3(1, currentLevel.transform.localScale.y, 1);
        currentLevel.transform.localScale = new Vector3(2, currentLevel.transform.localScale.y, 2);
        agent.SetDestination(currentLevel.transform.position);
    }

    Direction GetMovementDir() 
    {
        if (movement == Vector2Int.up) { return Direction.Up; }
        if (movement == Vector2Int.down) { return Direction.Down; }
        if (movement == Vector2Int.left) { return Direction.Left; }
        if (movement == Vector2Int.right) { return Direction.Right; }

        return Direction.Non;
    }

    private void OnEnterLevel(InputAction.CallbackContext context) 
    {
        if (currentLevel.scene == "") return;
        sceneManagerUI.SetAdditive(false);
        sceneManagerUI.SetCursorON(false);
        sceneManagerUI.OpenScene(currentLevel.scene);
    }
}