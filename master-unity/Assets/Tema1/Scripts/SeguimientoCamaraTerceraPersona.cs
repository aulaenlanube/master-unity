using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamaraTerceraPersona : MonoBehaviour
{
    public Transform objetivo;
    public float distancia = 5.0f;
    public float altura = 2.0f;
    public float suavizadoRotacion = 10.0f;
    public float suavizadoMovimiento = 2.0f;

    private Vector3 desplazamientoInicial;
    private float anguloY;
    private float anguloX;

    void Start()
    {
        // Guardar el desplazamiento inicial con respecto al objetivo
        desplazamientoInicial = transform.position - objetivo.position;
        // Bloquear el cursor en el centro y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) // Click derecho del ratón
        {
            anguloY += Input.GetAxis("Mouse X") * suavizadoRotacion;
            anguloX -= Input.GetAxis("Mouse Y") * suavizadoRotacion;
            anguloX = Mathf.Clamp(anguloX, -20, 80); // Limitar la rotación en X
        }

        // Rotar el desplazamiento inicial alrededor del eje Y y aplicar la rotación en X
        Quaternion rotacion = Quaternion.Euler(anguloX, anguloY, 0);
        Vector3 desplazamiento = rotacion * desplazamientoInicial;

        // Suavizar la posición de la cámara y mantener la altura
        Vector3 posicionObjetivo = objetivo.position + Vector3.up * altura + desplazamiento;
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizadoMovimiento * Time.deltaTime);

        // Asegurarse de que la cámara siempre mire al objetivo
        transform.LookAt(objetivo.position + Vector3.up * altura);
    }
}


