using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class BarraCorrerShooter : MonoBehaviour
{
    private float anchoMaximoBarra;
    private bool barraEstaActiva;

    void Start()
    {
        anchoMaximoBarra = GetComponent<RectTransform>().sizeDelta.x; // guardamos el ancho máximo de la barra basado en el tamaño inicial 
        barraEstaActiva = false;
        ActualizarEstadoBarraCorrer(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool debeEstarActiva = !MiniShooter.instance.BarraCorrerLlena();
        if (barraEstaActiva != debeEstarActiva)
        {
            ActualizarEstadoBarraCorrer(debeEstarActiva);
        }

        if(debeEstarActiva) ActualizarBarraCorrer();
    }

    void ActualizarBarraCorrer()
    {   
        // control barra correr
        float porcentajeTiempoCorrer = MiniShooter.instance.PorcentajeTiempoCorrer();
        GetComponent<RectTransform>().sizeDelta = new Vector2(anchoMaximoBarra * porcentajeTiempoCorrer, GetComponent<RectTransform>().sizeDelta.y);

        // cambiamos el color de la barra según el porcentaje restante
        if (porcentajeTiempoCorrer > 0.8f) GetComponent<Image>().color = Color.green;
        else if (porcentajeTiempoCorrer > 0.2f) GetComponent<Image>().color = Color.yellow;
        else GetComponent<Image>().color = Color.red;
    }

    void ActualizarEstadoBarraCorrer(bool activar)
    {
        GetComponent<Image>().enabled = activar;
        transform.parent.GetComponent<Image>().enabled = activar;
        barraEstaActiva = activar;
    }

}
