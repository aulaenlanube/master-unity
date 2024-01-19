using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    private ListaPuntuacionesJuegoPunteria puntuaciones;
    private string rutaFicheroPuntuaciones = Application.dataPath + "/Tema1/Scripts/Juego Diana/puntuaciones.json";
    private string rutaFicheroPuntuacionesBinario = Application.dataPath + "/Tema1/Scripts/Juego Diana/puntuaciones.dat";

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
        puntuaciones = new ListaPuntuacionesJuegoPunteria();
        CargarPuntuacionesBinario();
    }

    public List<int> ObtenerMejoresPuntuaciones()
    {
        return puntuaciones.listaPuntuaciones.OrderByDescending(x => x).ToList();
    }

    public List<int> ObtenerMejoresPuntuaciones(int cantidad)
    {
        return puntuaciones.listaPuntuaciones
            .OrderByDescending(x => x)
            .Take(cantidad)
            .ToList();
    }


    public void AgregarPuntuacion(int puntuacion)
    {
        puntuaciones.listaPuntuaciones.Add(puntuacion);
        GuardarPuntuacionesBinario();
    }

    public void CargarPuntuaciones()
    {
        if (File.Exists(rutaFicheroPuntuaciones))
        {
            string json = File.ReadAllText(rutaFicheroPuntuaciones);
            Debug.Log("cargar: " + json);
            puntuaciones = JsonUtility.FromJson<ListaPuntuacionesJuegoPunteria>(json) ?? new ListaPuntuacionesJuegoPunteria();
        }
    }

    private void GuardarPuntuaciones()
    {
        string json = JsonUtility.ToJson(puntuaciones);
        File.WriteAllText(rutaFicheroPuntuaciones, json);
    }

    public void GuardarPuntuacionesBinario()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(rutaFicheroPuntuacionesBinario);
        formatter.Serialize(file, puntuaciones);
        file.Close();
    }

    public void CargarPuntuacionesBinario()
    {        
        if (File.Exists(rutaFicheroPuntuacionesBinario))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(rutaFicheroPuntuacionesBinario, FileMode.Open);
            puntuaciones = (ListaPuntuacionesJuegoPunteria)formatter.Deserialize(file);
            file.Close();
        }        
    }


}

