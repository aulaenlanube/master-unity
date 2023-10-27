using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguidorRespawn : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5.0f;
    public float distanciaSeguimiento = 5.0f;
    public float distanciaRespawn = 1f;
    public Vector3[] posicionesRespawn;   

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.transform.position);
        Vector3 pos = transform.position;
        // respawn
        if (distanciaObjetivo < distanciaRespawn) 
        {
            transform.position = posicionesRespawn[Random.Range(0, posicionesRespawn.Length)];
        }
        // seguir
        else if (distanciaObjetivo < distanciaSeguimiento) 
        {
            transform.position = Vector3.MoveTowards(pos, objetivo.position, velocidad * Time.deltaTime);
        }
    }
}
