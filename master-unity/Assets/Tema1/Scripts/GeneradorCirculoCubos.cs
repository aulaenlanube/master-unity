using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCirculoCubos : MonoBehaviour
{
    public int numCubos = 10;  
    public float radio = 5f;  

    void Start()
    {
        for (int i = 0; i < numCubos; i++)
        {
            // posición angular en radianes
            float angulo = i * 2 * Mathf.PI / numCubos;

            // coordenadas x y z para cada cubo
            float x = Mathf.Cos(angulo) * radio;
            float z = Mathf.Sin(angulo) * radio;

            // crear cubo y posicionarlo
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.position = new Vector3(x, 0, z);
        }
    }
}
