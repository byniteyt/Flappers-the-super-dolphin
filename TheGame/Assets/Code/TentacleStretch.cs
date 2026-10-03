using UnityEngine;

public class TentacleIK : MonoBehaviour
{
    [Header("Bones")]
    [SerializeField] private Transform[] bones;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Shape")]
    [SerializeField] private float bend = 0f;

    [Header("Settings")]
    [SerializeField] private bool followTargetRotation = true;

    private float[] normalizedPositions;

    private void Awake()
    {
        if (bones == null || bones.Length < 2)
        {
            Debug.LogError("TentacleIK necesita al menos 2 huesos.");
            return;
        }

        normalizedPositions = new float[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            normalizedPositions[i] =
                (float)i / (bones.Length - 1);
        }
    }

    private void LateUpdate()
    {
        if (target == null || bones == null || bones.Length < 2)
            return;

        SolveTentacle();
    }

    private void SolveTentacle()
    {
        Vector3 start = bones[0].position;
        Vector3 end = target.position;

        Vector3 direction = end - start;

        float distance = direction.magnitude;

        if (distance < 0.0001f)
            return;

        direction.Normalize();

        // ==================================================
        // DIRECCIÓN DEL BEND
        // ==================================================

        // Usamos la orientación del propio tentáculo
        // para decidir hacia qué lado se curva.

        Vector3 bendDirection =
            Vector3.ProjectOnPlane(
                transform.up,
                direction
            );

        // Si transform.up es paralelo a direction,
        // usamos transform.right.
        if (bendDirection.sqrMagnitude < 0.0001f)
        {
            bendDirection =
                Vector3.ProjectOnPlane(
                    transform.right,
                    direction
                );
        }

        bendDirection.Normalize();

        // ==================================================
        // POSICIONES
        // ==================================================

        for (int i = 0; i < bones.Length; i++)
        {
            float t = normalizedPositions[i];

            // Línea recta entre la base y el target
            Vector3 position =
                Vector3.Lerp(
                    start,
                    end,
                    t
                );

            // Curva suave:
            //
            // t = 0   -> 0
            // t = 0.5 -> 1
            // t = 1   -> 0
            //
            // Por tanto la base y la punta permanecen
            // exactamente donde deben estar.

            float curve =
                Mathf.Sin(t * Mathf.PI);

            position +=
                bendDirection *
                bend *
                curve;

            bones[i].position = position;
        }

        // ==================================================
        // ROTACIÓN
        // ==================================================

        // NO tocamos las rotaciones intermedias.
        // Esto evita que el modelo se invierta.

        if (followTargetRotation)
        {
            bones[bones.Length - 1].rotation =
                target.rotation;
        }
    }
}