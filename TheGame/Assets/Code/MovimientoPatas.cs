using System.Collections;
using UnityEngine;

public class MovimientoPatas : MonoBehaviour
{
    [SerializeField] Transform objeto;
    [SerializeField] float gradosPorAjuste = 30f;
    [SerializeField] float distPorAjuste = 2; 
    [SerializeField] float velocidadGiro = 180f;
    [SerializeField] float alturaSalto = 0.5f;
    [SerializeField] float duracionMovimiento = 0.5f;

    [SerializeField] bool atrasado = false;
    float offsetAnimacion;

    [SerializeField] float extraFront;

    private float ultimoAngulo;
    private Vector3 ultimaPos; 
    private Coroutine corrutinaRotacion;

    bool desajustePos;

    [SerializeField] Transform target;

    void Start()
    {
        ultimoAngulo = transform.eulerAngles.y;
        ultimaPos = transform.position;

        Vector3 rotacionCangrejo = transform.eulerAngles;
        Vector3 rotacionObjeto = objeto.eulerAngles;

        rotacionCangrejo.x = 0;
        rotacionObjeto.x = 0;

        if (atrasado) offsetAnimacion = duracionMovimiento / 2;
    }

    public void SetTarget(Transform t) { target = t; }

    void Update()
    {
        float anguloActual = transform.eulerAngles.y;
        Vector3 posicionActual = transform.position;

        float diferenciaRot = Mathf.Abs(Mathf.DeltaAngle(ultimoAngulo,anguloActual));

        float diferenciaPos = Vector3.Distance(ultimaPos,posicionActual);

        desajustePos = diferenciaPos >= distPorAjuste;

        if (diferenciaRot >= gradosPorAjuste || desajustePos)
        {
            // Si ya hay una corrutina ejecutándose,
            // NO actualizamos las referencias todavía.
            if (corrutinaRotacion != null) return;

            ultimoAngulo = anguloActual;
            ultimaPos = posicionActual;

            corrutinaRotacion = StartCoroutine(AjustarObjeto());
        }
    }

    IEnumerator AjustarObjeto()
    {
        float anguloObjetivo = transform.eulerAngles.y;
        Vector3 posicionObjetivo = transform.position;

        float tiempo = 0f;

        // Posición inicial del objeto
        Vector3 posicionInicial = objeto.localPosition;

        Vector3 posicionObjetivoLocal = objeto.parent.InverseTransformPoint(transform.position);

        if (target != null)
        {
            // Dirección desde la pata hacia el objetivo, en World Space
            Vector3 dir = target.position - objeto.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.001f)
            {
                dir.Normalize();

                // Adelantar la pata hacia el objetivo
                Vector3 posicionObjetivoWorld = objeto.parent.TransformPoint(posicionObjetivoLocal);

                posicionObjetivoWorld += dir * extraFront;

                // Volver a convertir a Local Space
                posicionObjetivoLocal = objeto.parent.InverseTransformPoint(posicionObjetivoWorld);
            }
        }

        while (Mathf.Abs(Mathf.DeltaAngle(objeto.eulerAngles.y, anguloObjetivo)) > 0.1f
            || Vector3.Distance(objeto.localPosition, posicionObjetivoLocal) > 0.01f
            || tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;

            // =========================
            // GIRAR SOLO EN Y
            // =========================

            float nuevoAngulo = Mathf.MoveTowardsAngle(
                objeto.eulerAngles.y,
                anguloObjetivo,
                velocidadGiro * Time.deltaTime);

            objeto.rotation = Quaternion.Euler(0, nuevoAngulo, 0);

            // =========================
            // MOVIMIENTO DE POSICIÓN
            // =========================

            float progresoMovimiento = Mathf.Clamp01(tiempo / duracionMovimiento);

            Vector3 nuevaPosicion = Vector3.Lerp(posicionInicial,posicionObjetivoLocal,progresoMovimiento);

            // =========================
            // SUBIR Y BAJAR
            // =========================

            float progreso = Mathf.Clamp01((tiempo + offsetAnimacion) / duracionMovimiento);

            float altura = Mathf.Sin(progreso * Mathf.PI) * alturaSalto;

            // Aplicamos la posición + la elevación de la animación
            objeto.localPosition = nuevaPosicion + Vector3.up * altura;

            yield return null;
        }

        // =========================
        // ASEGURAR VALORES FINALES
        // =========================

        objeto.rotation = Quaternion.Euler(0, anguloObjetivo, 0);

        objeto.localPosition = posicionObjetivoLocal;

        corrutinaRotacion = null;
    }
}