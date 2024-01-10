using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventario : IInventario
{
    private Dictionary<string, ObjetoInventario> inventario;

    public List<ObjetoInventario> Objetos => inventario.Values.ToList();

    public Inventario()
    {
        inventario = new Dictionary<string, ObjetoInventario>();
    }

    public void MostrarInventario()
    {
        foreach (var objetoActualLista in inventario.Values)
        {    
            Debug.Log(objetoActualLista.ToString());            
        }
    }

    public void MostrarInventarioPorValor()
    {
        var objetosFiltrados = Objetos.OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log(objeto.ToString());
        }
    }

    public void MostrarInventarioPorRareza(Rareza rareza)
    {
        var objetosFiltrados = Objetos.Where(objeto => objeto.Rareza == rareza)
                                      .OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log(objeto.ToString());
        }
    }

    public void AgregarObjeto(ObjetoInventario objetoNuevo)
    {
        //if (inventario.ContainsKey(objetoNuevo.Nombre)) inventario[objetoNuevo.Nombre].Add(objetoNuevo);
        //else inventario.Add(objetoNuevo.Nombre, new List<ObjetoInventario>() { objetoNuevo });
    }

    public void EliminarObjeto(ObjetoInventario objetoBorrado)
    {
        /*if (inventario.ContainsKey(objetoBorrado.Nombre))
        {
            if (inventario[objetoBorrado.Nombre].Count > 1) inventario[objetoBorrado.Nombre].Remove(objetoBorrado);
            else if (inventario[objetoBorrado.Nombre].Count == 1) inventario.Remove(objetoBorrado.Nombre);
            return;
        }
        Debug.Log("El objeto con el Nombre proporcionado no existe en el inventario.");*/
    }
}





