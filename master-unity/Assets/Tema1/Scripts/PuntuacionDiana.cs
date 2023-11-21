using UnityEngine;

public class PuntuacionDiana : MonoBehaviour
{
    public int puntosPorImpacto;
    public void Impacto()
    {
        ControladorPuntuacionDianas.AgregarPuntos(puntosPorImpacto);
    }
}

