using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemplosCorrutinas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Coroutine c1 = StartCoroutine(CambiarColor(1));
        Coroutine c2 = StartCoroutine(MoverObjetoLerp(new Vector3(3,3,3), 3f));  
    }

    IEnumerator MiCorrutina()
    {
        Debug.Log("Visita aulaenlanube.com");
        yield return new WaitForSeconds(2);
        Debug.Log("2 segundos después...");
    }

    IEnumerator Parar(Coroutine c, float t)
    {
        yield return new WaitForSeconds(t);
        StopCoroutine(c);
    }

    IEnumerator CambiarColor(float n)
    {
        Renderer rend = GetComponent<Renderer>();
        while (true) // bucle infinito
        {
            rend.material.color = Color.red;
            yield return new WaitForSeconds(n);
            rend.material.color = Color.green;
            yield return new WaitForSeconds(n);
            rend.material.color = Color.blue;
            yield return new WaitForSeconds(n);
        }
    }

    IEnumerator Parpadear()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        float tiempoEspera = 0.2f;  // duración del parpadeo en segundos
        int numeroParpadeos = 10;   // número de veces que el objeto parpadeará

        for (int i = 0; i < numeroParpadeos; i++)
        {
            rend.enabled = !rend.enabled;               // invertir visibilidad 
            yield return new WaitForSeconds(tiempoEspera);  // esperamos         
        }
        rend.enabled = true;        // aseguramos que sea visible al final
    }

    IEnumerator MoverObjeto(Vector3 posicionObjetivo, float duracion)
    {
        Vector3 posicionInicial = transform.position;

        float tiempoTranscurrido = 0.0f;
        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracion;
            // calcular las nuevas coordenadas para cada eje
            float xNueva = posicionInicial.x + (posicionObjetivo.x - posicionInicial.x) * t;
            float yNueva = posicionInicial.y + (posicionObjetivo.y - posicionInicial.y) * t;
            float zNueva = posicionInicial.z + (posicionObjetivo.z - posicionInicial.z) * t;
            // actualizar la posición del objeto
            transform.position = new Vector3(xNueva, yNueva, zNueva);
            yield return null;
        }
        transform.position = posicionObjetivo; // aseguramos que el objeto llegue a su destino
    }

    IEnumerator MoverObjetoLerp(Vector3 posicionObjetivo, float duracion)
    {
        Vector3 posicionInicial = transform.position;
        float tiempoPasado = 0;

        while (tiempoPasado < duracion)
        {
            tiempoPasado += Time.deltaTime;
            transform.position = Vector3.Lerp(posicionInicial, posicionObjetivo, tiempoPasado / duracion);
            yield return null;
        }
    }

}
