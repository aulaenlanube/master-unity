using System;
using UnityEngine;

[Serializable]
public class RutaEnemigo
{
    public Vector3[] ruta; // Array de Vector3
}


public class RutasEnemigos : MonoBehaviour
{
    [SerializeField] private RutaEnemigo[] rutasEnemigos; // Array de RutaEnemigo
    private Color[] coloresRutas; // Array de colores para las rutas

    public static RutasEnemigos instance;

    private void Awake()
    {
        instance = this;        
    }

    private void Start()
    {
        coloresRutas = new Color[rutasEnemigos.Length]; 
        for (int i = 0; i < rutasEnemigos.Length; i++)
        {
            coloresRutas[i] = GenerarColorClaro(); // generamos un color aleatorio para cada ruta
        }
    }

    // método para obtener una ruta aleatoria de un enemigo
    public Vector3[] ObtenerRutaAleatoria()
    {
        int indice = UnityEngine.Random.Range(0, rutasEnemigos.Length);
        return rutasEnemigos[indice].ruta;
    }

    //método para obtener una posición aleatoria de una ruta de un enemigo, utilizado para el respawn
    public Vector3 ObtenerPosicionAleatoria()
    {
        int indiceRuta = UnityEngine.Random.Range(0, rutasEnemigos.Length);
        int indicePunto = UnityEngine.Random.Range(0, rutasEnemigos[indiceRuta].ruta.Length);
        return rutasEnemigos[indiceRuta].ruta[indicePunto];
    }

    // Genera un color RGB aleatorio claro
    public static Color GenerarColorClaro()
    {
        float minValor = 0.5f; // valor mínimo para cada componente del color

        // genera valores aleatorios para cada componente dentro del rango [minValor, 1]
        float r = UnityEngine.Random.Range(minValor, 1f);
        float g = UnityEngine.Random.Range(minValor, 1f);
        float b = UnityEngine.Random.Range(minValor, 1f);
        
        return new Color(r, g, b);
    }


    // método para visualizar las distintas rutas de los enemigos con Gizmos
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < rutasEnemigos.Length; i++)
            {
                Gizmos.color = coloresRutas[i]; 

                for (int j = 0; j < rutasEnemigos[i].ruta.Length - 1; j++)
                {
                    Gizmos.DrawLine(rutasEnemigos[i].ruta[j], rutasEnemigos[i].ruta[j + 1]);
                }
            }
        }
    }
}
