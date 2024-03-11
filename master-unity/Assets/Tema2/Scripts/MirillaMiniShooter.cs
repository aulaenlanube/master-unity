using UnityEngine;
using UnityEngine.UI;

public class MirillaMiniShooter : MonoBehaviour
{
    private Vector2 escalaOriginalMirilla;
    private Vector2 escalaZoomMirilla;

    private void Start()
    {
        escalaOriginalMirilla = MiniShooter.instance.ObtenerEscalaOriginalMirilla();
        escalaZoomMirilla = escalaOriginalMirilla * .5f;
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) //bot�n derecho del mouse
        {
            // apuntamos con la mirilla
            Apuntar();
        }
        else
        {  
            // desapuntamos con la mirilla
            Desapuntar();
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

            //zoom de la c�mara
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
                                                 MiniShooter.instance.CapacidadZoom,
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
