using UnityEngine;
using UnityEngine.UI;

public class MirillaMiniShooter : MonoBehaviour
{
    private Vector2 escalaOriginalMirilla;
    private Vector2 escalaZoomMirilla;

    private Camera camaraPrincipal;
    private Camera camaraApuntado;

    private void Start()
    {
        camaraPrincipal = MiniShooter.instance.CamaraPrincipal;
        camaraApuntado = MiniShooter.instance.CamaraApuntando;

        camaraPrincipal.enabled = true;
        camaraApuntado.enabled = false;

        escalaOriginalMirilla = MiniShooter.instance.ObtenerEscalaOriginalMirilla();
        escalaZoomMirilla = escalaOriginalMirilla * 0.5f;
    }

    void Update()
    {
        // Si el jugador no está corriendo y se mantiene presionado el botón derecho del mouse
        if (Input.GetMouseButton(1) && !MiniShooter.instance.EstaCorriendo())
        {
            Apuntar();
        }
        else
        {
            Desapuntar();
        }
    }

    public void Apuntar()
    {
        Image mirilla = MiniShooter.instance.MirillaActual();

        // Hacer que la mirilla se haga más pequeña para simular el zoom
        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaZoomMirilla, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        // Cambiar al sprite de zoom si la escala está al 90% del tamaño objetivo
        if (escalaZoomMirilla.x / mirilla.rectTransform.sizeDelta.x > 0.9f)
        {
            // en primera persona, cambiamos el sprite de la mirilla y la cámara
            if (MiniShooter.instance.EstaEnPrimeraPersona())
            {
                MiniShooter.instance.MirillaPrimeraPersona.enabled = false;
                CambiarACamaraApuntado();
            }
            else
            {
                //en tercera persona capacidad zoom, por defecto es X2
                int capacidadZoom = MiniShooter.instance.EstaEnPrimeraPersona() ? MiniShooter.instance.CapacidadZoom : (int)CapacidadZoom.X2;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, capacidadZoom, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

            }
        }
    }

    public void Desapuntar()
    {
        // en primera persona, cambiamos el sprite de la mirilla y la cámara
        if (MiniShooter.instance.EstaEnPrimeraPersona())
        {
            MiniShooter.instance.MirillaPrimeraPersona.enabled = true;
            CambiarACamaraPrincipal();
        }

        // retornar suavemente la mirilla a su escala y capacidad de zoom original
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, (float)CapacidadZoom.SinZoom, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        Image mirilla = MiniShooter.instance.MirillaActual();
        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginalMirilla, Time.deltaTime * MiniShooter.instance.VelocidadZoom);

    }

    void CambiarACamaraApuntado()
    {
        camaraApuntado.gameObject.GetComponent<Animator>().SetBool("apuntar", true);
    }

    void CambiarACamaraPrincipal()
    {
        camaraApuntado.gameObject.GetComponent<Animator>().SetBool("apuntar", false);
    }
}
