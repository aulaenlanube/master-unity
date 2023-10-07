using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguidor : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 2f;
    public float distanciaSeguimiento = 5f;
    public float distanciaEvasion = 2f;

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

        // condicional para seguir o evitar al objetivo
        if (distanciaObjetivo < distanciaEvasion)
        {
            // evitar al objetivo
            Vector3 direccion = (transform.position - objetivo.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (distanciaObjetivo < distanciaSeguimiento)
        {
            // seguir al objetivo
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.red;
        }
    }


}
