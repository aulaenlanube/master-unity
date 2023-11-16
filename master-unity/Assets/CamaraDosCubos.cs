using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDosCubos : MonoBehaviour
{

    public GameObject cubo1;
    public GameObject cubo2;
    public float margen = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 puntoMedio = (cubo1.transform.position + cubo2.transform.position) / 2;
        float distancia = (cubo1.transform.position - cubo2.transform.position).magnitude + margen;
        distancia = Mathf.Max(distancia, 10f);
        transform.position = puntoMedio - transform.forward * distancia;        
    }
}
