using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorLaberinto : MonoBehaviour
{
    public GameObject cubo;
    public GameObject suelo;
    public int filas = 21; // Debe ser impar
    public int columnas = 21; // Debe ser impar
    public float tamañoCelda = 1.0f;

    private int[,] laberinto;

    void Start()
    {
        laberinto = new int[filas, columnas];
        InicializarLaberinto();
        GenerarLaberinto(2, 2);
        DibujarLaberinto();
        CrearEntradaSalida();
    }

    void InicializarLaberinto()
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                laberinto[i, j] = 1;
            }
        }
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
                Vector3 posicion = new Vector3(i * tamañoCelda, 0, j * tamañoCelda);
                GameObject aInstanciar = laberinto[i, j] == 1 ? cubo : suelo;
                if(laberinto[i, j] != 1) { posicion += new Vector3(0, -0.55f, 0);  }

                GameObject instancia = Instantiate(aInstanciar, posicion, Quaternion.identity);
                instancia.transform.parent = this.transform;
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

