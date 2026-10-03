using UnityEngine;

public class LookAtDelay : MonoBehaviour
{
    Transform target;
    [SerializeField] float spinSpeed;

    [SerializeField] float limiteVertical = 30f;

    Timer timer = new Timer(0.01f);

    [SerializeField] float extraY = 1;

    private void Awake()
    {
        target = FindFirstObjectByType<FlappersClient>().transform;
    }

    private void Update()
    {
        timer.UpdateTimer();

        if (timer.isRunning()) return;

        timer.StartTimer();

        Vector3 direccion = new Vector3(target.position.x, target.position.y + extraY, target.position.z) - transform.position;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

            Vector3 euler = rotacionObjetivo.eulerAngles;

            // Convertir 0-360 a -180/180
            float pitch = Mathf.DeltaAngle(0, euler.x);

            // Limitar arriba/abajo
            pitch = Mathf.Clamp(pitch, -limiteVertical, limiteVertical);

            Quaternion rotacionLimitada = Quaternion.Euler(
                pitch,
                euler.y,
                0
            );

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                rotacionLimitada,
                spinSpeed * Time.deltaTime
            );
        }
    }
}