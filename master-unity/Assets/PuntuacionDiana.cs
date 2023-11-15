using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuacionDiana : MonoBehaviour
{
    public int puntosPorImpacto;

    // Llamado desde el script de control cuando este objeto es impactado.
    public void Impacto()
    {
        ControladorPuntuacionDianas.AgregarPuntos(puntosPorImpacto);
    }
}

