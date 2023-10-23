using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoArray : MonoBehaviour
{
    public Vector3[] puntos;
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoverEntrePuntos(puntos, velocidad));        
    }

    IEnumerator MoverEntrePuntos(Vector3[] puntos, float velocidad)
    {
        if (puntos.Length < 2) yield break;
        int indiceSiguientePunto = 0;
        Vector3 inicio = transform.position;
        while (true)
        {
            Vector3 destino = puntos[indiceSiguientePunto];
            transform.position = Vector3.MoveTowards(inicio, destino, velocidad * Time.deltaTime);
            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                inicio = destino;
                indiceSiguientePunto = ++indiceSiguientePunto % puntos.Length;
            }
            else inicio = transform.position;
            yield return null;
        }
    }

}
