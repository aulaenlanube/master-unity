using UnityEngine;
using UnityEngine.UI;

public class TextoClicsRecursividad : MonoBehaviour
{
    private void OnEnable()
    {
        JuegoRecursividad.clicsActualizados += ActualizarClics;
    }

    private void OnDisable()
    {
        JuegoRecursividad.clicsActualizados -= ActualizarClics;
    }

    private void ActualizarClics(int clicsActuales, int clicsTotales)
    {
        if(clicsActuales == clicsTotales) GetComponent<Text>().text = $"Fin partida";
        else GetComponent<Text>().text = $"{clicsActuales}/{clicsTotales}";
    }
}
