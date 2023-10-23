using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneradorCorrutinas : MonoBehaviour
{
    public int lado = 10;
    public float separacion = 1.1f;
    public float intervalo = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        GenerarCubos();
    }

    private void GenerarCubos()
    {
        for (int z = 0; z < lado; z++)
        {
            for (int x = 0; x < lado; x++)
            {
                GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubo.transform.position = new Vector3(x * separacion, 0, z * separacion);
                cubo.AddComponent<EscalarAlturaCubo>();
            }
        }
    }

    IEnumerator CrearCuardado1()
    {
        for (int z = 0; z < lado; z++)
        {
            for (int x = 0; x < lado; x++)
            {
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }
        }          
    }

    IEnumerator CrearCuardado2()
    {
        for (int z = 0; z < lado; z++)
        {
            for (int x = 0; x < lado; x++)
            {
                CrearCubo(z, x);
                yield return new WaitForSeconds(intervalo);
            }
        }
    }
IEnumerator CrearCuardadoZigZag()
{
    bool izquierdaADerecha = true;
    for (int z = 0; z < lado; z++)
    {
        if (izquierdaADerecha)
        {
            for (int x = 0; x < lado; x++)
            {
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }
        }
        else
        {
            for (int x = lado - 1; x >= 0; x--)
            {
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }
        }

        izquierdaADerecha = !izquierdaADerecha;
    }
}


    void CrearCubo(int x, int z)
    {
        GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubo.transform.position = new Vector3(x*separacion, 0, z*separacion);
    }

    private IEnumerator GenerarCuadradoEspiral()
    {        
        int x = 0;
        int z = 0;
        int capas = (lado + 1) / 2;

        for (int i = 0; i < capas; i++)
        {
            // derecha
            for (int j = 0; j < lado - i * 2; j++)
            {
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
                if (j < lado - i * 2 - 1) x++;
            }

            // arriba
            for (int j = 0; j < lado - i * 2 - 1; j++)
            {
                z++;
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }

            // izquierda
            for (int j = 0; j < lado - i * 2 - 1; j++)
            {
                x--;
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }

            // abajo
            for (int j = 0; j < lado - i * 2 - 2; j++)
            {
                z--;
                CrearCubo(x, z);
                yield return new WaitForSeconds(intervalo);
            }

            x++;
        }
    }

    private IEnumerator GenerarCuadradoEspiral2()
    {
        Vector2Int direccion = Vector2Int.right; // Empezamos moviéndonos a la derecha.
        Vector2Int posicion = new Vector2Int(0, 0);
        int pasos = 1;

        // Total de cubos a crear.
        int totalCubos = lado * lado;

        for (int i = 0; i < totalCubos;)
        {
            for (int j = 0; j < 2; j++) // Dos direcciones por cada tamaño de pasos.
            {
                for (int p = 0; p < pasos && i < totalCubos; p++) // Crear 'pasos' cubos en la dirección actual.
                {
                    CrearCubo(posicion.x, posicion.y);
                    yield return new WaitForSeconds(intervalo);

                    // Avanzar en la dirección actual.
                    posicion += direccion;
                    i++; // Incrementar el conteo de cubos creados.
                }

                // Rotar la dirección 90 grados a la derecha.
                direccion = new Vector2Int(direccion.y, -direccion.x);
            }

            pasos++; // Aumentar el tamaño de pasos para la siguiente iteración.
        }
    }



}
