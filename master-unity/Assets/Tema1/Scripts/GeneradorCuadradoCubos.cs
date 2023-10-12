using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCuadradoCubos : MonoBehaviour
{
    public float lado = 10f;  
    public int numCubos = 20; 

    void Start()
    {
        float perimetro = 4 * lado;  // perímetro del cuadrado
        float espacio = perimetro / numCubos;  // espacio entre cada cubo

        for (int i = 0; i < numCubos; i++)
        {
            float posActual = i * espacio;  // posición actual a lo largo del perimetro
            Vector3 posicionCubo = Vector3.zero;

            // bordes del cuadrado: inferior, derecho, superior e izquierdo
            if (posActual < lado)
            {
                posicionCubo.x = posActual;
            }
            else if (posActual < 2 * lado)
            {
                posicionCubo.x = lado;
                posicionCubo.z = posActual - lado;
            }
            else if (posActual < 3 * lado)
            {
                posicionCubo.x = 3 * lado - posActual;
                posicionCubo.z = lado;
            }
            else
            {
                posicionCubo.z = 4 * lado - posActual;
            }

            // crear cubo
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.position = posicionCubo;
        }
    }
}
