using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovTriangulo : MonoBehaviour
{
    public float velocidad = 5f;
    public float lado = 10f;

    //constantes para la dirección 
    private const int DERECHA = 0, ARRIBA_IZQUIERDA = 1, ABAJO_IZQUIERDA = 2;   
    private int direccion = DERECHA;
    private Vector3 inicio;
    private Vector3 puntoObjetivo;
    private Vector3 puntoA, puntoB, puntoC;

    void Start()
    {
        inicio = transform.position;

        puntoA = inicio;
        puntoB = new Vector3(lado, 0, 0);
        puntoC = new Vector3(lado / 2, 0, Mathf.Sqrt(3) * lado / 2);

        puntoObjetivo = puntoB; // se inicia con movimiento hacia la derecha
    }

    void Update()
    {
        Vector3 movimiento = (puntoObjetivo - transform.position).normalized * velocidad * Time.deltaTime;

        if (Vector3.Distance(transform.position, puntoObjetivo) > velocidad * Time.deltaTime)
        {
            transform.position += movimiento;
        }  
        else
        {
            transform.position = puntoObjetivo;

            // actualiza la dirección y el punto objetivo
            switch (direccion)
            {
                case DERECHA:
                    puntoObjetivo = puntoC;
                    direccion = ARRIBA_IZQUIERDA;
                    break;
                case ARRIBA_IZQUIERDA:
                    puntoObjetivo = puntoA;
                    direccion = ABAJO_IZQUIERDA;
                    break;
                case ABAJO_IZQUIERDA:
                    puntoObjetivo = puntoB;
                    direccion = DERECHA;
                    break;
            }
        }
    }
}
