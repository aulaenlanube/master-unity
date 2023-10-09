using System.Collections;
using UnityEngine;

public class MovPosiciones : MonoBehaviour
{
    public Vector3[] posicionesObjetivo = { 
        new Vector3(0, 0, 0),
        new Vector3(1, 1, 0),
        new Vector3(2, 0, 2),
        new Vector3(3, 1, 3),
        new Vector3(4, 0, 4)
    };
    
    public float tiempoParaMover = 1f;

    void Start()
    {
        StartCoroutine(Mover());
    }

    IEnumerator Mover()
    {
        while (true) // Bucle infinito para reiniciar la secuencia
        {
            foreach (Vector3 posicionObjetivo in posicionesObjetivo)
            {   
                transform.position = posicionObjetivo;
                yield return new WaitForSeconds(tiempoParaMover); 
            }
        }
    }

}
