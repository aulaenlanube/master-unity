using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
[Serializable] public class ListaPuntuacionesJuegoPunteria
{
    public List<int> listaPuntuaciones;

    public ListaPuntuacionesJuegoPunteria()
    {
        listaPuntuaciones = new List<int>();
    }
}

public class PuntuacionesJuegoPunteria
{    
    private static PuntuacionesJuegoPunteria instancia = null;
    private ListaPuntuacionesJuegoPunteria puntuacionesContenedor;
    private string rutaFicheroPuntuaciones = Application.dataPath + "/Tema1/Scripts/Juego Diana/puntuaciones.json";

    public static PuntuacionesJuegoPunteria Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new PuntuacionesJuegoPunteria();
            }
            return instancia;
        }
    }

    private PuntuacionesJuegoPunteria()
    {
        puntuacionesContenedor = new ListaPuntuacionesJuegoPunteria();
        CargarPuntuaciones();
    }

    public List<int> ObtenerMejoresPuntuaciones()
    {
        return puntuacionesContenedor.listaPuntuaciones.OrderByDescending(x => x).ToList();
    }

    public List<int> ObtenerMejoresPuntuaciones(int cantidad)
    {
        return puntuacionesContenedor.listaPuntuaciones
            .OrderByDescending(x => x)
            .Take(cantidad)
            .ToList();
    }


    public void AgregarPuntuacion(int puntuacion)
    {
        puntuacionesContenedor.listaPuntuaciones.Add(puntuacion);
        GuardarPuntuaciones();
    }

    public void CargarPuntuaciones()
    {
        if (File.Exists(rutaFicheroPuntuaciones))
        {
            string json = File.ReadAllText(rutaFicheroPuntuaciones);
            Debug.Log("cargar: " + json);
            puntuacionesContenedor = JsonUtility.FromJson<ListaPuntuacionesJuegoPunteria>(json) ?? new ListaPuntuacionesJuegoPunteria();
        }
    }

    private void GuardarPuntuaciones()
    {
        string json = JsonUtility.ToJson(puntuacionesContenedor);
        File.WriteAllText(rutaFicheroPuntuaciones, json);
    }
}

