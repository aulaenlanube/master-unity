using UnityEngine;
using UnityEngine.UI;

public class ControladorPuntuacionDianas : MonoBehaviour
{
    private delegate void ImpactoDiana();
    private  static event ImpactoDiana impactoRecibido;
    private static int puntuacionTotal;

    public static void AgregarPuntos(int puntos)
    {
        puntuacionTotal += puntos;
        impactoRecibido.Invoke();         
    }

    private void OnEnable()
    {
        impactoRecibido += ActualizarPuntuacion;
    }

    private void ActualizarPuntuacion()
    {
        GetComponent<Text>().text = $"Puntos: {puntuacionTotal}";
    }
}
