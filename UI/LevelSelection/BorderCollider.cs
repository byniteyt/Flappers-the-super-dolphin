using UnityEngine;

public class BorderCollider : MonoBehaviour
{
    [SerializeField] FlappersLevelSelector flappers;
    [SerializeField] LevelSelectionCamera cam;

    [SerializeField] Transform pos1, pos2;

    bool left;

    private void Start()
    {
        left = flappers.transform.position.x < transform.position.x;
    }

    private void FixedUpdate()
    {
        bool currentLeft = flappers.transform.position.x < transform.position.x;

        if (currentLeft != left)
        {
            left = currentLeft;

            Transform dir = left ? pos1 : pos2;
            cam.StartMove(dir);
        }
    }
}