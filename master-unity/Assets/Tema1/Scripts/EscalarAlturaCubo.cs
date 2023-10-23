using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalarAlturaCubo : MonoBehaviour
{
    public float duracion = 0.5f;
    public float escalaMinima = 1f;
    public float escalaMaxima = 5f;
    private Vector3 escalaInicial;
    private Vector3 escalaObjetivo;

    private void Start()
    {
        escalaInicial = new Vector3(1, 1, 1);
        StartCoroutine(AlterarEscala());
    }

    private IEnumerator AlterarEscala()
    {
        while (true)
        {
            float escalaYObjetivo = Random.Range(escalaMinima, escalaMaxima);
            escalaObjetivo = new Vector3(1, escalaYObjetivo, 1);

            // escalar hacia arriba
            yield return CambiarEscala(escalaInicial, escalaObjetivo);
            // escalar hacia abajo
            yield return CambiarEscala(escalaObjetivo, escalaInicial);
        }
    }

    private IEnumerator CambiarEscala(Vector3 desde, Vector3 hasta)
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = tiempoTranscurrido / duracion;

            // interpolación lineal para modificar la escala
            float escalaY = Mathf.SmoothStep(desde.y, hasta.y, progreso);
            transform.localScale = new Vector3(1, escalaY, 1);

            // ajustamos posición para asegurar que la base del cubo siempre permanezca en el eje y = 0
            transform.position = new Vector3(transform.position.x, escalaY / 2, transform.position.z);
            cambiarColor();

            yield return null;
            //escalaInicial = transform.localScale;            
        }
    }

    private void cambiarColor()
    {
        if (transform.localScale.y >= escalaMaxima * 0.9f)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (transform.localScale.y < escalaMinima + escalaMaxima * 0.1f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}