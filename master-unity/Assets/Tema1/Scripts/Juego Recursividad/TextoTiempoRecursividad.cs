using UnityEngine;
using UnityEngine.UI;

public class TextoTiempoRecursividad : MonoBehaviour
{
    private void OnEnable()
    {
        JuegoRecursividad.tiempoActualizado += ActualizarTiempo;
    }

    private void OnDisable()
    {
        JuegoRecursividad.tiempoActualizado -= ActualizarTiempo;
    }

    private void ActualizarTiempo(float tiempo)
    {        
        GetComponent<Text>().text = $"{tiempo:F2}s";
    }
}
