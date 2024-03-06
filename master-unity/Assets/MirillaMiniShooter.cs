using UnityEngine;
using UnityEngine.UI;
using static Diana;

[RequireComponent(typeof(Image))]
public class MirillaMiniShooter : MonoBehaviour
{
    [SerializeField] private GameObject mirillaZoom;
    [SerializeField] private float velocidadZoom = 20f;
    [SerializeField] private float capacidadZoom = 30;

    private Image mirilla;
    private Vector2 escalaOriginal;
    private Vector2 escalaZoom;
    private bool zoomActivo = false;

    void Start()
    {
        mirilla = GetComponent<Image>();
        escalaOriginal = mirilla.rectTransform.sizeDelta;
        escalaZoom = escalaOriginal * 0.5f; // se asume que el tama�o de zoom es la mitad del original
    }

    void Update()
    {
        if (MiniShooter.instance.EstaEnPrimeraPersona())
        {
            if (Input.GetMouseButton(1)) //bot�n derecho del mouse
            {
                //activamos zoom
                zoomActivo = true;

                // hacer que la mirilla se haga m�s peque�a para simular el zoom
                mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginal * 0.5f, Time.deltaTime * velocidadZoom); // Asume que quieres reducir a la mitad el tama�o original

                // cambiar al sprite de zoom si el tama�o est� al 80% del tama�o objetivo
                if (escalaZoom.x / mirilla.rectTransform.sizeDelta.x > 0.8f)
                {
                    GetComponent<Image>().enabled = false;
                    mirillaZoom.GetComponent<Image>().enabled = true;

                    //zoom de la c�mara
                    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, capacidadZoom, Time.deltaTime * 10);
                }
            }
            else
            {
                if (zoomActivo)
                {  
                    //quitamos zoom de la c�mara
                    zoomActivo = false;
                    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, 1);
                }

                GetComponent<Image>().enabled = true;
                mirillaZoom.GetComponent<Image>().enabled = false;

                // Retornar suavemente la mirilla a su posici�n y tama�o original            
                mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginal, Time.deltaTime * velocidadZoom);

                
            }
        }

    }

    //---------------------------------------------
    //------------------ EVENTOS ------------------
    //---------------------------------------------
    private void OnEnable()
    {
        MiniShooter.camaraCambiada += ActualizarMirilla;
    }

    private void OnDisable()
    {
        MiniShooter.camaraCambiada -= ActualizarMirilla;
    }

    public void ActualizarMirilla(int indice)
    {
        if (indice != 0)
        {
            GetComponent<Image>().enabled = false;
            mirillaZoom.GetComponent<Image>().enabled = false;
        }
    }
}
