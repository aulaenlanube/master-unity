
using UnityEngine;

public class CamaraMirilla : MonoBehaviour
{
    public Camera camaraPrincipal;
    public Camera camaraApuntado;

    void Start()
    {
        // Aseg�rate de que la c�mara principal sea la activa al comienzo
        camaraPrincipal.enabled = true;
        camaraApuntado.enabled = false;

        // Establecer la etiqueta MainCamera en la c�mara principal
        camaraPrincipal.tag = "MainCamera";
        camaraApuntado.tag = "Untagged";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Detecta cuando se presiona el clic derecho
        {
            CambiarACamaraApuntado();
        }

        if (Input.GetMouseButtonUp(1)) // Detecta cuando se suelta el clic derecho
        {
            CambiarACamaraPrincipal();
        }
    }

    void CambiarACamaraApuntado()
    {
        camaraPrincipal.enabled = false;
        camaraApuntado.enabled = true;

        // Cambiar la etiqueta MainCamera a la c�mara de apuntado
        camaraPrincipal.tag = "Untagged";
        camaraApuntado.tag = "MainCamera";
    }

    void CambiarACamaraPrincipal()
    {
        camaraApuntado.enabled = false;
        camaraPrincipal.enabled = true;

        // Cambiar la etiqueta MainCamera a la c�mara principal
        camaraApuntado.tag = "Untagged";
        camaraPrincipal.tag = "MainCamera";
    }
}
