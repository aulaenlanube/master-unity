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
    private string rutaFicheroPuntuacionesJson = Application.dataPath + "/Tema1/Scripts/Juego Diana/puntuaciones.json";
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
        CargarPuntuacionesJson();
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
        GuardarPuntuacionesJson();
    }

    public void CargarPuntuacionesJson()
    {
        if (File.Exists(rutaFicheroPuntuacionesJson))
        {
            string json = File.ReadAllText(rutaFicheroPuntuacionesJson);
            puntuaciones = JsonUtility.FromJson<ListaPuntuacionesJuegoPunteria>(json);
            return;
        }
        puntuaciones = new ListaPuntuacionesJuegoPunteria();
    }

    private void GuardarPuntuacionesJson()
    {
        string json = JsonUtility.ToJson(puntuaciones);
        File.WriteAllText(rutaFicheroPuntuacionesJson, json);
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
            return;
        }
        puntuaciones = new ListaPuntuacionesJuegoPunteria();
    }


}

