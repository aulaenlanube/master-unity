using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamaraDetras : MonoBehaviour
{
    public Transform objetivo; 				        // objeto que la cámara seguirá
    public float distancia = 10.0f; 		        // distancia entre la cámara y el objeto
    public Vector3 offset = new Vector3(0, 0, 0);	// Offset adicional

    void Update()
    {
        if (objetivo != null)
        {

            Vector3 offsetRotado = objetivo.rotation * new Vector3(0, offset.y, -distancia);
            transform.position = objetivo.position + offsetRotado;

            transform.LookAt(objetivo.position + new Vector3(0, offset.y, 0));
        }
    }
}
