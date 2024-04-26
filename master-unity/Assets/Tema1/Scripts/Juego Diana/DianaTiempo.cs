using UnityEngine;
using UnityEngine.UI;

public class DianaTiempo : MonoBehaviour
{
    private void OnEnable()
    {
        Diana.tiempoActualizado += ActualizarTiempo;
    }

    private void OnDisable()
    {
        Diana.tiempoActualizado -= ActualizarTiempo;
    }

    private void ActualizarTiempo(float tiempo)
    {
        if (tiempo == 0)
        {
            GetComponent<Text>().text = "Fin de la partida, pulsa 'S' para salir";
        }
        else GetComponent<Text>().text = $"Tiempo restante: {tiempo:F2}";

        if(tiempo < 3) GetComponent<Text>().color = Color.red;
    }
}
