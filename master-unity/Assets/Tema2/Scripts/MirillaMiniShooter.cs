using UnityEngine;
using UnityEngine.UI;

public class MirillaMiniShooter : MonoBehaviour
{
    private Vector2 escalaOriginalMirilla;
    private Vector2 escalaZoomMirilla;
    private Animator animatorPrimeraPersona;

    private void Start()
    {
        escalaOriginalMirilla = MiniShooter.instance.ObtenerEscalaOriginalMirilla();
        escalaZoomMirilla = escalaOriginalMirilla * .5f;
        animatorPrimeraPersona = MiniShooter.instance.PersonajePrimeraPersona.GetComponent<Animator>();
    }

    void Update()
    {
        // si el jugador no est� corriendo y se mantiene presionado el bot�n derecho del mouse
        if (Input.GetMouseButton(1) && !MiniShooter.instance.EstaCorriendo())
        {
            // apuntamos con la mirilla
            Apuntar();
            animatorPrimeraPersona.SetBool("apuntando", true);
        }
        else
        {  
            // desapuntamos con la mirilla
            Desapuntar();
            animatorPrimeraPersona.SetBool("apuntando", false);
        }
    }

    public void Apuntar()
    {

        Image mirilla = MiniShooter.instance.MirillaActual();

        // hacer que la mirilla se haga m�s peque�a para simular el zoom
        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta,
                                                       escalaZoomMirilla,
                                                       Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        // cambiar al sprite de zoom si la escala est� al 90% del tama�o objetivo
        if (escalaZoomMirilla.x / mirilla.rectTransform.sizeDelta.x > .9f)
        {
            //si est� en primera persona, cambiamos el sprite de la mirilla
            if (MiniShooter.instance.EstaEnPrimeraPersona())
            {
                MiniShooter.instance.MirillaZoomPrimeraPersona.enabled = true;
                MiniShooter.instance.MirillaPrimeraPersona.enabled = false;
            }

            int capacidadZoom = MiniShooter.instance.EstaEnPrimeraPersona() ?  MiniShooter.instance.CapacidadZoom : (int)CapacidadZoom.X2;

            //zoom de la c�mara
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
                                                 capacidadZoom,
                                                 Time.deltaTime * MiniShooter.instance.VelocidadZoom);
        }
    }

    public void Desapuntar()
    {

        //si est� en primera persona, cambiamos el sprite de la mirilla
        if (MiniShooter.instance.EstaEnPrimeraPersona())
        {
            MiniShooter.instance.MirillaZoomPrimeraPersona.enabled = false;
            MiniShooter.instance.MirillaPrimeraPersona.enabled = true;
        }

        // retornar suavemente la mirilla a su escala original 
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
                                             (float)CapacidadZoom.SinZoom,
                                             Time.deltaTime * MiniShooter.instance.VelocidadZoom);

        Image mirilla = MiniShooter.instance.MirillaActual();

        mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta,
                                                       escalaOriginalMirilla,
                                                       Time.deltaTime * MiniShooter.instance.VelocidadZoom);

    }
}
