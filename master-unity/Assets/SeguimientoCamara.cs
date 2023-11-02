using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform objetivo; 				        // objeto que la cámara seguirá
    public float distancia = 10.0f; 		        // distancia entre la cámara y el objeto
    public Vector3 offset = new Vector3(0, 0, 0);	// Offset adicional

    void Update()
    {
        if (objetivo != null)
        {
            // calcular la nueva posición
            Vector3 nuevaPos = objetivo.position - (objetivo.forward * distancia) + offset;

            // actualizar la posición de la cámara
            transform.position = nuevaPos;
        }
    }
}

