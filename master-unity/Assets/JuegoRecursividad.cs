using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoRecursividad : MonoBehaviour
{
    [Range(5, 10)]
    public int dimension;
    [Range(0f, 0.1f)]
    public float separacion;
    private GameObject[,] cuadriculaCubos;

    void Start()
    {
        dimension = dimension * 2 + 1;
        cuadriculaCubos = new GameObject[dimension, dimension];
        GenerarCuadricula();
        Camera.main.transform.position = cuadriculaCubos[dimension / 2, dimension / 2].transform.position - Vector3.forward * dimension;
    }

    void GenerarCuadricula()
    {
        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                Vector3 posicion = new Vector3(x + x * separacion, y + y * separacion, 0);
                GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubo.transform.position = posicion;
                cubo.name = $"Cubo_{x}_{y}";
                //cubo.AddComponent<Cubo>();
                cuadriculaCubos[x, y] = cubo;
                CambiarColorCubo(cubo);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                GameObject cuboSeleccionado = hit.collider.gameObject;
                int conteo = ContarCubosMismoColor(cuboSeleccionado);
                Debug.Log($"Cubos del mismo color alrededor: {conteo}");
                CambiarColoresAleatoriamente();
            }
        }
    }

    int ContarCubosMismoColor(GameObject cuboSeleccionado)
    {
        int conteo = 0;
        Color colorCuboSeleccionado = cuboSeleccionado.GetComponent<Renderer>().material.color;

        // Encuentra la posición del cubo en la cuadrícula
        int posX = 0, posY = 0;
        bool encontrado = false;
        for (int x = 0; x < dimension && !encontrado; x++)
        {
            for (int y = 0; y < dimension && !encontrado; y++)
            {
                if (cuadriculaCubos[x, y] == cuboSeleccionado)
                {
                    posX = x;
                    posY = y;
                    encontrado = true;
                }
            }
        }

        // Verifica los cubos adyacentes
        for (int x = Mathf.Max(0, posX - 1); x <= Mathf.Min(dimension - 1, posX + 1); x++)
        {
            for (int y = Mathf.Max(0, posY - 1); y <= Mathf.Min(dimension - 1, posY + 1); y++)
            {
                // Ignora el cubo seleccionado
                if (x == posX && y == posY) continue;

                GameObject cuboAdyacente = cuadriculaCubos[x, y];
                Color colorCuboAdyacente = cuboAdyacente.GetComponent<Renderer>().material.color;

                if (colorCuboAdyacente == colorCuboSeleccionado)
                {
                    conteo++;
                }
            }
        }

        return conteo;
    }


    void CambiarColoresAleatoriamente()
    {
        foreach (GameObject cubo in cuadriculaCubos)
        {
            CambiarColorCubo(cubo);
        }
    }

    void CambiarColorCubo(GameObject cubo)
    {
        Renderer rendererCubo = cubo.GetComponent<Renderer>();
        Color colorAleatorio = ObtenerColorAleatorio();
        rendererCubo.material.color = colorAleatorio;
    }

    Color ObtenerColorAleatorio()
    {
        Color[] colores = { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.gray, new Color(0.5f, 0, 0.5f) }; // Magenta para morado, gris para marrón
        return colores[Random.Range(0, colores.Length)];
    }
}
