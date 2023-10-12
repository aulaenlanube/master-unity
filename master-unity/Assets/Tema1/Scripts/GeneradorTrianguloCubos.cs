using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorTrianguloCubos : MonoBehaviour
{
    public float lado = 4f;  // lado del triángulo
    public int numCubos = 27; // total de cubos

    void Start()
    {
        float espacio = 3 * lado / numCubos;  // espacio entre cada cubo
        float altura = Mathf.Sqrt(3) * lado / 2;

        for (int i = 0; i < numCubos; i++)
        {
            float posActual = i * espacio;  // posición actual a lo largo del perimetro
            Vector3 posCubo = Vector3.zero;

            // lados del triángulo: 1, 2 y 3
            if (posActual < lado)
            {
                posCubo.x = posActual;
            }
            else if (posActual < 2 * lado)
            {
                posCubo.x = lado - (posActual - lado) * 0.5f;
                posCubo.z = (posActual - lado) * altura / lado;
            }
            else
            {
                posCubo.x = lado - (posActual - lado) * 0.5f;
                posCubo.z = altura - (posActual - 2 * lado) * altura / lado;
            }

            // crear cubo
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.position = posCubo;
        }
    }
}
