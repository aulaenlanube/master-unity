using OpenCover.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventario : IInventario
{
    private HashSet<ObjetoInventario> inventario;

    public Inventario()
    {
        inventario = new HashSet<ObjetoInventario>();
    }

    public bool EstaEnInventario(ObjetoInventario objetoInventario)
    {
        return inventario.Contains(objetoInventario);
    }

    public int ObtenerCantidadMaterialBasico(TipoMaterialBasico tipoMaterialBasico)
    {
        // buscar si ya existe un material básico del mismo tipo
        MaterialBasico materialBasicoExistente = (MaterialBasico)inventario.FirstOrDefault(objeto => objeto is MaterialBasico materialBasico && materialBasico.TipoMaterialBasico == tipoMaterialBasico);

        if (materialBasicoExistente != null) return materialBasicoExistente.Cantidad;
        return 0;
    }

    public void AgregarObjeto(ObjetoInventario objetoNuevo)
    {
        if (objetoNuevo is MaterialBasico)
        {
            Debug.Log($"Para agregar un material básico al inventario se requiere la cantidad. Utiliza el método AgregarMaterialBasico");
            return;
        }

        // caso general
        inventario.Add(objetoNuevo);
    }

    public void EliminarObjeto(ObjetoInventario objetoBorrado)
    {
        if (objetoBorrado is MaterialBasico)
        {
            Debug.Log($"No se puede eliminar un material básico se requiere la cantidad. Utiliza el método EliminarMaterialBasico");
            return;
        }

        // caso general
        if (inventario.Remove(objetoBorrado)) Debug.Log($"El objeto {objetoBorrado.Nombre} ha sido eliminado del inventario");
        else Debug.Log($"El objeto {objetoBorrado.Nombre} no se encuentra en el inventario");

    }

    public void MostrarInventario()
    {
        foreach (var objetoActualLista in inventario)
        {
            Debug.Log(objetoActualLista.ToString());
        }
    }

    public MaterialBasico ObtenerMaterialBasico(TipoMaterialBasico tipoMaterialBasico) //revisar si es necesario
    {
        // buscar si ya existe un material básico del mismo tipo
        MaterialBasico materialBasico;
        foreach(ObjetoInventario obj in inventario)
        {
            if(obj is MaterialBasico)
            {
                materialBasico = (MaterialBasico)obj;
                if (materialBasico.TipoMaterialBasico == tipoMaterialBasico) return materialBasico;
            }
        }
        return null;        
    }

    public void MostrarInventarioPorValor()
    {
        var objetosFiltrados = inventario.OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log(objeto.ToString());
        }
    }

    public void MostrarInventarioPorRareza(Rareza rareza)
    {
        var objetosFiltrados = inventario.Where(objeto => objeto.Rareza == rareza)
                                      .OrderByDescending(objeto => objeto.CosteOro)
                                      .ThenByDescending(objeto => objeto.CostePlata)
                                      .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetosFiltrados)
        {
            Debug.Log(objeto.ToString());
        }
    }

    public void MostrarObjetosCategoria(Type tipoObjeto)
    {
        var objetos = inventario.Where(objeto => objeto.GetType() == tipoObjeto)
                                .OrderByDescending(objeto => objeto.CosteOro)
                                .ThenByDescending(objeto => objeto.CostePlata)
                                .ThenByDescending(objeto => objeto.CosteBronce);

        foreach (var objeto in objetos)
        {
            Debug.Log(objeto.ToString());
        }
    }

    public void MostrarArmas()
    {
        MostrarObjetosCategoria(typeof(Arma));
    }

    public void MostrarArmaduras()
    {
        MostrarObjetosCategoria(typeof(Armadura));
    }

    public void MostrarConsumibles()
    {
        MostrarObjetosCategoria(typeof(Consumible));
    }

    public void MostrarArtefactos()
    {
        MostrarObjetosCategoria(typeof(Artefacto));
    }

    public void MostrarMaterialesBasicos()
    {
        MostrarObjetosCategoria(typeof(MaterialBasico));
    }

    // solución alternativa para AGREGAR materiales básicos
    public void AgregarMaterialBasico(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        // buscar si ya existe un material básico del mismo tipo
        MaterialBasico materialBasicoExistente = ObtenerMaterialBasico(tipoMaterialBasico);

        if (materialBasicoExistente != null)
        {
            // si existe, incrementar la cantidad del nuevo al existente
            materialBasicoExistente.IncrementarCantidad(cantidad);
            return;
        }
        // si no existe, crear un nuevo objeto
        inventario.Add(new MaterialBasico(tipoMaterialBasico, null, cantidad));
    }


    // solución alternativa para ELIMINAR materiales básicos
    public void EliminarMaterialBasico(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        //validamos la cantidad
        if (cantidad <= 0)
        {
            Debug.Log($"No se puede eliminar una cantidad negativa de {tipoMaterialBasico}");
            return;
        }

        // buscar si ya existe un material básico del mismo tipo
        MaterialBasico materialBasicoExistente = ObtenerMaterialBasico(tipoMaterialBasico);

        if (materialBasicoExistente != null)
        {
            // comprobar que la cantidad a borrar no sea mayor que la existente
            if (materialBasicoExistente.Cantidad < cantidad)
            {
                Debug.Log($"No se puede eliminar {cantidad} unidades de {materialBasicoExistente.Nombre} porque solo hay {materialBasicoExistente.Cantidad} unidades");
                return;
            }

            // si existe, restar la cantidad del borrado al existente
            materialBasicoExistente.DecrementarCantidad(cantidad);
        }
    }





}





