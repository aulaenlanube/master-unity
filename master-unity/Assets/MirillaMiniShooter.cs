using UnityEngine;

public class MirillaMiniShooter : MonoBehaviour
{         
    private Vector2 escalaOriginal;
    private bool zoomActivo = false;

    void Start()
    {   
        escalaOriginal = MiniShooter.instance.Mirilla.rectTransform.sizeDelta;
    }

    void Update()
    {
        if (MiniShooter.instance.EstaEnPrimeraPersona())
        {
            if (Input.GetMouseButton(1)) //bot�n derecho del mouse
            {
                // activamos zoom y establecemos la escala de zoom
                zoomActivo = true;
                Vector2 escalaZoom = escalaOriginal * .5f;

                // hacer que la mirilla se haga m�s peque�a para simular el zoom
                MiniShooter.instance.Mirilla.rectTransform.sizeDelta = Vector2.Lerp(MiniShooter.instance.Mirilla.rectTransform.sizeDelta, 
                                                                                    escalaZoom, 
                                                                                    Time.deltaTime * MiniShooter.instance.VelocidadZoom); 

                // cambiar al sprite de zoom si la escala est� al 90% del tama�o objetivo
                if (escalaZoom.x / MiniShooter.instance.Mirilla.rectTransform.sizeDelta.x > .9f)
                {
                    MiniShooter.instance.Mirilla.enabled = false;
                    MiniShooter.instance.MirillaZoom.enabled = true;

                    //zoom de la c�mara
                    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 
                                                         (float)MiniShooter.instance.CapacidadZoom, 
                                                         Time.deltaTime * MiniShooter.instance.VelocidadZoom);
                }
            }
            else
            {
                if (zoomActivo) //quitamos zoom de la c�mara
                {                      
                    zoomActivo = false;   
                    MiniShooter.instance.Mirilla.enabled = true;
                    MiniShooter.instance.MirillaZoom.enabled = false;                    
                }

                // retornar suavemente la mirilla a su escala original 
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 
                                                     (float)CapacidadZoom.SinZoom, 
                                                     Time.deltaTime * MiniShooter.instance.VelocidadZoom);
                
                MiniShooter.instance.Mirilla.rectTransform.sizeDelta = Vector2.Lerp(MiniShooter.instance.Mirilla.rectTransform.sizeDelta, 
                                                                                    escalaOriginal, 
                                                                                    Time.deltaTime * MiniShooter.instance.VelocidadZoom);                    
            }
        }
    }
}
