using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SeguidorCircular : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5.0f;
    public float radio = 10.0f;
    public int cantidadPuntosCirculo = 10;
    public float distanciaSeguimiento = 5.0f;
    public float distanciaEvasion = 2.0f;

    private Vector3 puntoCentral;
    private Vector3 puntoActualCirculo;
    private Vector3[] puntosCirculo;
    private int indice = 0;

    void Start()
    {
        puntoCentral = transform.position;    
        puntosCirculo = new Vector3[cantidadPuntosCirculo];
        for (int i = 0; i < cantidadPuntosCirculo; i++)
        {
            float angulo = i * 2 * Mathf.PI / cantidadPuntosCirculo;
            float x = Mathf.Cos(angulo) * radio;
            float z = Mathf.Sin(angulo) * radio;
            puntosCirculo[i] = new Vector3(puntoCentral.x + x, transform.position.y, puntoCentral.z + z);
        }
        puntoActualCirculo = puntosCirculo[indice];
    }

    void Update()
    {
        float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivo.position);
        Vector3 pos = transform.position;

        if (distanciaAlObjetivo < distanciaEvasion) // evitar al objetivo
        {
            transform.position = Vector3.MoveTowards(pos, pos - objetivo.position, velocidad * Time.deltaTime);
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (distanciaAlObjetivo < distanciaSeguimiento) // seguir al objetivo
        {
            transform.position = Vector3.MoveTowards(pos, objetivo.position, velocidad * Time.deltaTime);
            GetComponent<Renderer>().material.color = Color.red;
        }
        else // volver al último punto de seguimiento y seguir el patrón circular
        {
            if (Vector3.Distance(pos, puntoActualCirculo) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(pos, puntoActualCirculo, velocidad * Time.deltaTime);
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                indice = ++indice % cantidadPuntosCirculo;
                puntoActualCirculo = puntosCirculo[indice];                
            }
        }
    }
}
