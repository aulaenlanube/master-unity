using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DianaPuntuaciones : MonoBehaviour
{
    private void OnEnable()
    {
        Diana.partidaFinalizada += FinalizarPartida;
    }

    private void OnDisable()
    {
        Diana.partidaFinalizada -= FinalizarPartida;
    }

    private void FinalizarPartida(int puntuacion)
    {
        //habilitamos el text de la UI para los marcadores
        GetComponent<Text>().enabled = true;

        //guardamos puntuación
        PuntuacionesJuegoPunteria.Instancia.AgregarPuntuacion(puntuacion);

        //mostramos las mejores puntuaciones
        List<int> mejoresPuntuaciones = PuntuacionesJuegoPunteria.Instancia.ObtenerMejoresPuntuaciones(5);
        string textoMejoresPuntuaciones = "Mejores puntuaciones:\n";
        foreach (int puntuacionActual in mejoresPuntuaciones)
        {
            textoMejoresPuntuaciones += puntuacionActual + "\n";
        }
        GetComponent<Text>().text = textoMejoresPuntuaciones;
    }
}
