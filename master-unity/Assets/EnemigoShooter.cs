using TMPro;
using UnityEngine;

public class EnemigoShooter : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5.0f;
    public TextMeshProUGUI textoFinPartida;

    public int ancho = 40;
    public int profundidad = 40;

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.transform.position);
        Vector3 pos = transform.position;

        // sigue al personaje
        if (distanciaObjetivo > 1)
        {
            transform.position = Vector3.MoveTowards(pos, objetivo.position, velocidad * Time.deltaTime);
        }
        // si algún enemigo se acerca mucho, termina partida
        else
        {
            textoFinPartida.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void DestruirObjetivo()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        CambiarPosicionObjetivo();
    }

    public void CambiarPosicionObjetivo()
    {
        transform.position = new Vector3(
            Random.Range(-ancho, ancho),
            transform.position.y,
            Random.Range(-profundidad, profundidad));
    }
}
