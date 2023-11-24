using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorLaberinto : MonoBehaviour
{
    [Tooltip("El valor controla el tamaño del laberinto")]
    [Range(1,10)]
    public int dimension;

    private int filas;
    private int columnas; 
    private float dimensionCelda = 1f;
    private int[,] laberinto;

    void Start()
    {
        filas = dimension*10+1;
        columnas = dimension*10+1;
        laberinto = new int[filas, columnas];
        InicializarLaberinto();
        GenerarLaberinto(2, 2);
        DibujarLaberinto();
        CrearEntradaSalida();
    }

    void InicializarLaberinto()
    {
        for (int i = 0; i < filas; i++)        
            for (int j = 0; j < columnas; j++)            
                laberinto[i, j] = 1;  
    }

    void GenerarLaberinto(int x, int y)
    {
        int[][] direcciones = new int[][] {
            new int[] {0, 1},
            new int[] {1, 0},
            new int[] {0, -1},
            new int[] {-1, 0}
        };

        Shuffle(direcciones);

        foreach (int[] direccion in direcciones)
        {
            int nuevoX = x + direccion[0] * 2;
            int nuevoY = y + direccion[1] * 2;

            if (nuevoX >= 0 && nuevoX < filas && nuevoY >= 0 && nuevoY < columnas && laberinto[nuevoX, nuevoY] == 1)
            {
                laberinto[nuevoX, nuevoY] = 0;
                laberinto[x + direccion[0], y + direccion[1]] = 0;
                GenerarLaberinto(nuevoX, nuevoY);
            }
        }
    }

    void Shuffle(int[][] direcciones)
    {
        for (int i = 0; i < direcciones.Length; i++)
        {
            int tempIndex = Random.Range(0, direcciones.Length);
            int[] temp = direcciones[i];
            direcciones[i] = direcciones[tempIndex];
            direcciones[tempIndex] = temp;
        }
    }

    void DibujarLaberinto()
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (laberinto[i, j] == 1)
                {
                    Vector3 posicion = new Vector3(i * dimensionCelda, 0, j * dimensionCelda);                    
                    GameObject instancia = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    instancia.transform.position = posicion;
                    instancia.transform.parent = this.transform;
                }
            }
        }
    }

    void CrearEntradaSalida()
    {
        laberinto[0, 1] = 0;
        laberinto[filas - 1, columnas - 2] = 0;
        Destroy(GameObject.Find("Suelo(Clone)"), 0.1f);
        Destroy(GameObject.Find("Suelo(Clone)"), 0.1f);
    }
}

