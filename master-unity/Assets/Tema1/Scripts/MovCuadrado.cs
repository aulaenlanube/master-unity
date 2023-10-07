using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCuadrado : MonoBehaviour
{
    public float velocidad = 5f;
    public float lado = 10f;

    private const int ARRIBA = 0, ABAJO = 1, IZQUIERDA = 2, DERECHA = 3;
    private Vector3 inicio;
    private int direccion = DERECHA;

    void Start()
    {
        inicio = transform.position;
    }

    void Update()
    {        
        Vector3 mov = direccion switch
        {
            DERECHA => new Vector3(1,0,0),
            ARRIBA => new Vector3(0, 0, 1),
            IZQUIERDA => new Vector3(-1, 0, 0),
            ABAJO => new Vector3(0, 0, -1),
            _ => Vector3.zero
        };

        transform.position += mov * velocidad * Time.deltaTime;

        if (direccion == DERECHA && transform.position.x >= inicio.x + lado) direccion = ARRIBA;
        else if (direccion == ARRIBA && transform.position.z >= inicio.z + lado) direccion = IZQUIERDA;
        else if (direccion == IZQUIERDA && transform.position.x <= inicio.x) direccion = ABAJO;
        else if (direccion == ABAJO && transform.position.z <= inicio.z) direccion = DERECHA;
    }
}
