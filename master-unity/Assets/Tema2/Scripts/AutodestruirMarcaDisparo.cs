using System.Collections;
using UnityEngine;

public class AutodestruirMarcaDisparo : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida = 5;
    [SerializeField] private float duracionEscalado = 15;
    private float tiempoInicio = 0;
    private float escalaInicial;
    private bool reduciendo = false;

    private void Start() 
    { 
        escalaInicial = transform.localScale.x; 
    }

    void Update()
    {
        tiempoInicio += Time.deltaTime;
        if (tiempoInicio >= tiempoDeVida && !reduciendo)
        {
            reduciendo = true;
            StartCoroutine(ReducirEscala());
        }
    }

    private IEnumerator ReducirEscala()
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionEscalado)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = tiempoTranscurrido / duracionEscalado;
            float escala = Mathf.Lerp(escalaInicial, 0, progreso);
            transform.localScale = new Vector3(escala, escala, 1);
            yield return null;
        }
        Destroy(gameObject);
    }
}
