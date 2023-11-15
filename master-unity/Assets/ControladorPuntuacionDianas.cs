using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuntuacionDianas : MonoBehaviour
{
    public static int puntuacionTotal;

    public static void AgregarPuntos(int puntos)
    {
        puntuacionTotal += puntos;
        // Aquí puedes actualizar la UI o cualquier otro sistema que necesite saber la puntuación.
        Debug.Log("Puntuación actual: " + puntuacionTotal);
    }
}
