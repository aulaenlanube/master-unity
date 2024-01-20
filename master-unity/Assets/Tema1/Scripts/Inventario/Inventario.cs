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

    public MaterialBasico ObtenerMaterialBasico(TipoMaterialBasico tipoMaterialBasico)
    {
        // buscar si ya existe un material básico del mismo tipo, null si no existe
        return (MaterialBasico)inventario.FirstOrDefault(objeto => objeto is MaterialBasico materialBasico && materialBasico.TipoMaterialBasico == tipoMaterialBasico);
    }

    public int ObtenerCantidadMaterialBasico(TipoMaterialBasico tipoMaterialBasico)
    {
        return ObtenerMaterialBasico(tipoMaterialBasico)?.Cantidad ?? 0;
    }

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

    public void EliminarMaterialBasico(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        //validamos la cantidad
        if (cantidad <= 0)
        {
            Debug.Log($"No se puede eliminar una cantidad negativa de {tipoMaterialBasico}");
            return;
        }

        // comprobar que la cantidad a borrar no sea mayor que la existente
        if (ObtenerCantidadMaterialBasico(tipoMaterialBasico) < cantidad)
        {
            Debug.Log($"No se puede eliminar {cantidad} unidades de {tipoMaterialBasico} porque no hay suficientes unidades");
            return;
        }

        // si existe, restar la cantidad del borrado al existente
        ObtenerMaterialBasico(tipoMaterialBasico).DecrementarCantidad(cantidad);
    }

    

    ///-----------------------------------------------------------------------
    //-- MÉTODOS MOSTRAR INVENTARIO ------------------------------------------
    //------------------------------------------------------------------------
    public void MostrarInventario()
    {
        foreach (var objetoActualLista in inventario)
        {
            Debug.Log(objetoActualLista.ToString());
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
     
    public void MostrarInventarioPorValor()
    {
        inventario.OrderByDescending(objeto => objeto.Precio.CosteOro)
                  .ThenByDescending(objeto => objeto.Precio.CostePlata)
                  .ThenByDescending(objeto => objeto.Precio.CosteBronce)
                  .ToList()
                  .ForEach(objeto => Debug.Log(objeto));
    }

    public void MostrarInventarioPorRareza(Rareza rareza)
    {
        inventario.Where(objeto => objeto.Rareza == rareza)
                  .OrderByDescending(objeto => objeto.Precio.CosteOro)
                  .ThenByDescending(objeto => objeto.Precio.CostePlata)
                  .ThenByDescending(objeto => objeto.Precio.CosteBronce)
                  .ToList()
                  .ForEach(objeto => Debug.Log(objeto));
    }

    public void MostrarObjetosCategoria(Type tipoObjeto)
    {
        inventario.Where(objeto => objeto.GetType() == tipoObjeto)
                  .OrderByDescending(objeto => objeto.Precio.CosteOro)
                  .ThenByDescending(objeto => objeto.Precio.CostePlata)
                  .ThenByDescending(objeto => objeto.Precio.CosteBronce)
                  .ToList()
                  .ForEach(objeto => Debug.Log(objeto));
    }
}





