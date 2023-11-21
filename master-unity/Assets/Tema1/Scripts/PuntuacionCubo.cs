using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionCubo : MonoBehaviour
{
    public Text textoPuntuacion;
    public int puntos = 0;

    // Start is called before the first frame update
    void Start()
    {
        ActualizarPuntuacion();        
    }

    public void AumentarPuntuacion(int n)
    {
        puntos += n;
        ActualizarPuntuacion();
        Moneda.ComprobarPartidaFinalizada(puntos);
    }

    private void ActualizarPuntuacion()
    {
        textoPuntuacion.text = $"{gameObject.name}: {puntos}";
    }   

   
}
