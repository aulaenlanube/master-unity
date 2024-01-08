using UnityEngine;
using UnityEngine.UI;

public class TextoPuntosRecursividad : MonoBehaviour
{
    private void OnEnable()
    {
        JuegoRecursividad.puntuacionActualizada += ActualizarPuntuacion;
    }

    private void OnDisable()
    {
        JuegoRecursividad.puntuacionActualizada -= ActualizarPuntuacion;
    }

    private void ActualizarPuntuacion(int puntos)
    {
        GetComponent<Text>().text = $"Puntos:{puntos}";
    }
}
