using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MirillaMiniShooter : MonoBehaviour
{
    [SerializeField] private GameObject mirillaZoom;
    [SerializeField] private float velocidadZoom = 20f;    

    private Image mirilla; 
    private Vector2 escalaOriginal;
    private Vector2 escalaZoom;

    void Start()
    {
        mirilla = GetComponent<Image>();
        escalaOriginal = mirilla.rectTransform.sizeDelta;
        escalaZoom = escalaOriginal * 0.5f; // se asume que el tamaño de zoom es la mitad del original
    }

    void Update()
    {        
        if (Input.GetMouseButton(1)) //botón derecho del mouse
        {  
            // hacer que la mirilla se haga más pequeña para simular el zoom
            mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginal * 0.5f, Time.deltaTime * velocidadZoom); // Asume que quieres reducir a la mitad el tamaño original

            // cambiar al sprite de zoom si el tamaño está al 80% del tamaño objetivo
            if (escalaZoom.x/mirilla.rectTransform.sizeDelta.x > 0.8f)
            {
                GetComponent<Image>().enabled = false;
                mirillaZoom.GetComponent<Image>().enabled = true;
            }
        }
        else
        {            
            GetComponent<Image>().enabled = true;
            mirillaZoom.GetComponent<Image>().enabled = false;

            // Retornar suavemente la mirilla a su posición y tamaño original            
            mirilla.rectTransform.sizeDelta = Vector2.Lerp(mirilla.rectTransform.sizeDelta, escalaOriginal, Time.deltaTime * velocidadZoom);
        }
    }
}
