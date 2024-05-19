using UnityEngine;
using UnityEngine.UI;

public class MirillaMiniShooter : MonoBehaviour
{
    private Vector2 escalaOriginalMirilla;
    private Vector2 escalaZoomMirilla;

    public Camera camaraPrincipal;
    public Camera camaraApuntado;

    private bool enTransicion = false;
    private float tiempoTransicion = 1.0f; // Duraci�n de la transici�n en segundos
    private float tiempoTranscurrido = 0.0f;
    private float campoDeVisionInicial;
    private float campoDeVisionObjetivo;

    private void Start()
    {
        // Aseg�rate de que la c�mara principal sea la activa al comienzo
        camaraPrincipal.enabled = true;
        camaraApuntado.enabled = false;

        // Establecer la etiqueta MainCamera en la c�mara principal
        camaraPrincipal.tag = "MainCamera";
        camaraApuntado.tag = "Untagged";

        escalaOriginalMirilla = MiniShooter.instance.ObtenerEscalaOriginalMirilla();
        escalaZoomMirilla = escalaOriginalMirilla * 0.5f;
    }

    void Update()
    {
        if (enTransicion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = Mathf.Clamp01(tiempoTranscurrido / tiempoTransicion);

            Camera.main.fieldOfView = Mathf.Lerp(campoDeVisionInicial, campoDeVisionObjetivo, t);

            if (t >= 1.0f)
            {
                enTransicion = false;
            }
        }

        // Si el jugador no est� corriendo y se mantiene presionado el bot�n derecho del mouse
        if (Input.GetMouseButton(1) && !MiniShooter.instance.EstaCorriendo())
        {
            CambiarACamaraApuntado();
            Apuntar();
        }
        else
        {
            Desapuntar();
            CambiarACamaraPrincipal();
        }
    }

    public void Apuntar()
    {
        Image mirilla = MiniShooter.instance.MirillaActual();

        // Hacer que la mirilla se haga m�s peque�a para simular el zoom
        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaZoomMirilla, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        // Cambiar al sprite de zoom si la escala est� al 90% del tama�o objetivo
        if (escalaZoomMirilla.x / mirilla.rectTransform.sizeDelta.x > 0.9f)
        {
            // Si est� en primera persona, cambiamos el sprite de la mirilla
            if (MiniShooter.instance.EstaEnPrimeraPersona())
            {
                MiniShooter.instance.MirillaPrimeraPersona.enabled = false;
            }

            int capacidadZoom = MiniShooter.instance.EstaEnPrimeraPersona() ? MiniShooter.instance.CapacidadZoom : (int)CapacidadZoom.X2;

            // Zoom de la c�mara
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, capacidadZoom, Time.deltaTime * MiniShooter.instance.VelocidadZoom);
        }
    }

    public void Desapuntar()
    {
        // Si est� en primera persona, cambiamos el sprite de la mirilla
        if (MiniShooter.instance.EstaEnPrimeraPersona())
        {
            MiniShooter.instance.MirillaPrimeraPersona.enabled = true;
        }

        // Retornar suavemente la mirilla a su escala original
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, (float)CapacidadZoom.SinZoom, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        Image mirilla = MiniShooter.instance.MirillaActual();
        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginalMirilla, Time.deltaTime * MiniShooter.instance.VelocidadZoom);
    }

    void CambiarACamaraApuntado()
    {
        if (camaraPrincipal.enabled)
        {
            enTransicion = true;
            tiempoTranscurrido = 0.0f;
            campoDeVisionInicial = camaraPrincipal.fieldOfView;
            campoDeVisionObjetivo = camaraApuntado.fieldOfView;
        }

        camaraPrincipal.enabled = false;

        camaraApuntado.enabled = true;
        camaraApuntado.GetComponent<Animator>().enabled = true;

        // Cambiar la etiqueta MainCamera a la c�mara de apuntado
        camaraPrincipal.tag = "Untagged";
        camaraApuntado.tag = "MainCamera";
    }

    void CambiarACamaraPrincipal()
    {
        if (camaraApuntado.enabled)
        {
            enTransicion = true;
            tiempoTranscurrido = 0.0f;
            campoDeVisionInicial = camaraApuntado.fieldOfView;
            campoDeVisionObjetivo = camaraPrincipal.fieldOfView;
        }

        camaraApuntado.enabled = false;
        camaraApuntado.GetComponent<Animator>().enabled = false;

        camaraPrincipal.enabled = true;

        // Cambiar la etiqueta MainCamera a la c�mara principal
        camaraApuntado.tag = "Untagged";
        camaraPrincipal.tag = "MainCamera";
    }
}
