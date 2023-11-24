using UnityEngine;
using UnityEngine.UI;

public class DianaPuntuacion : MonoBehaviour
{
    private void OnEnable()
    {
        Diana.puntuacionActualizada += ActualizarPuntuacion;
    }

    private void OnDisable()
    {
        Diana.puntuacionActualizada -= ActualizarPuntuacion;
    }

    private void ActualizarPuntuacion(int puntos)
    {
        GetComponent<Text>().text = $"Puntuación: {puntos}";
    }
}
