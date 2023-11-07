using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamaraPrimeraPersona : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float sensibilidad = 100.0f; // Sensibilidad del movimiento del ratón

    private float anguloVertical = 0.0f;
    private float anguloHorizontal = 0.0f;


    void Update()
    {
        // Obtener la entrada del ratón para la rotación
        anguloHorizontal += Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        anguloVertical -= Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        anguloVertical = Mathf.Clamp(anguloVertical, -89f, 89f); // Limita el vuelco de la cámara

        // Cambia la rotación del jugador para que coincida con la rotación horizontal de la cámara
        jugador.rotation = Quaternion.Euler(0, anguloHorizontal, 0);


        // Ajusta la posicióny rotación de la cámara para la vista en primera persona
        Quaternion rotacionCompleta = Quaternion.Euler(anguloVertical, anguloHorizontal, 0);
        transform.position = jugador.position + rotacionCompleta * Vector3.forward;
        transform.rotation = rotacionCompleta;

    }
}



