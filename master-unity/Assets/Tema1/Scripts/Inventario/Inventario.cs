using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventario
{
    private Dictionary<string, List<ObjetoInventario>> inventario;

    public Inventario()
    {
        inventario = new Dictionary<string, List<ObjetoInventario>>();
    }

    public void MostrarInventario()
    {
        foreach (var listaActualObjetos in inventario.Values)
        {
            foreach (var objetoActualLista in listaActualObjetos)
            {
                Debug.Log(objetoActualLista.ToString());
            }
        }
    }

    public void MostrarInventarioPorValor()
    {
        var objetosFiltrados = inventario.SelectMany(par => par.Value).ToList()
                                      .OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log($"Nombre: {objeto.Nombre}, Valor: {objeto.CosteOro} oro, {objeto.CostePlata} plata, {objeto.CosteBronce} bronce, Descripción: {objeto.Descripcion}");
        }
    }

    public void MostrarInventarioPorRareza(Rareza rareza)
    {
        var objetosFiltrados = inventario.SelectMany(par => par.Value).ToList().Where(objeto => objeto.Rareza == rareza)
                                      .OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log($"Nombre: {objeto.Nombre}, Rareza: {objeto.Rareza}, Valor: {objeto.CosteOro} oro, {objeto.CostePlata} plata, {objeto.CosteBronce} bronce, Descripción: {objeto.Descripcion}");
        }
    }

    public void AgregarObjeto(ObjetoInventario objetoNuevo)
    {
        if (inventario.ContainsKey(objetoNuevo.Nombre)) inventario[objetoNuevo.Nombre].Add(objetoNuevo);
        else inventario.Add(objetoNuevo.Nombre, new List<ObjetoInventario>() { objetoNuevo });

    }

    public void EliminarObjeto(ObjetoInventario objetoBorrado)
    {
        if (inventario.ContainsKey(objetoBorrado.Nombre))
        {
            if (inventario[objetoBorrado.Nombre].Count > 1) inventario[objetoBorrado.Nombre].Remove(objetoBorrado);
            else if (inventario[objetoBorrado.Nombre].Count == 1) inventario.Remove(objetoBorrado.Nombre);
            return;
        }
        Debug.Log("El objeto con el Nombre proporcionado no existe en el inventario.");
    }
}





