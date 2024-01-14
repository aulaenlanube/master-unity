using UnityEngine;
using TMPro;

public class SeguidorRespawn : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5.0f;
    public float distanciaSeguimiento = 100f;
    public float distanciaRespawn = 1f;
    public Vector3[] posicionesRespawn;
    public TextMeshProUGUI textoFinPartida; 

    //eventos para actualizar la cantidad de respawns y la posición de respawn
    public delegate void EventoRespawn(int n, Vector3 v);
    public static event EventoRespawn respawn;
    private int cantidadRespawns = 0;

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.transform.position);
        Vector3 pos = transform.position;

        // respawn
        if (distanciaObjetivo < distanciaRespawn) 
        {
            transform.position = posicionesRespawn[Random.Range(0, posicionesRespawn.Length)];

            //lanzamos los eventos al actualizar la cantidad de respawns
            respawn?.Invoke(++cantidadRespawns, transform.position);            
          
        }
        // seguir
        else if (distanciaObjetivo < distanciaSeguimiento) 
        {
            transform.position = Vector3.MoveTowards(pos, objetivo.position, velocidad * Time.deltaTime);
        }
    }
}
