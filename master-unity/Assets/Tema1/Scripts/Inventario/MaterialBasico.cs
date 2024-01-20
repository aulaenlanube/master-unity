using System.Collections.Generic;
using UnityEngine;

 public enum TipoMaterialBasico
{
    Oro,
    Plata,
    Bronce,
    Hierro,
    Acero,
    Aluminio,
    Cobre,
    Madera,
    Cuero,
    Piedra,
    Diamante,
}


public static class PreciosMaterialesBasicos
{
    private static readonly Dictionary<TipoMaterialBasico, Precio> costes = new Dictionary<TipoMaterialBasico, Precio>
    {
        { TipoMaterialBasico.Oro, new Precio(1, 0, 0) },
        { TipoMaterialBasico.Plata, new Precio(0, 1, 0) },
        { TipoMaterialBasico.Bronce, new Precio(0, 0, 1) },
        { TipoMaterialBasico.Hierro, new Precio(0, 0, 1) },
        { TipoMaterialBasico.Acero, new Precio(0, 0, 2) },
        { TipoMaterialBasico.Aluminio, new Precio(0, 0, 1) },
        { TipoMaterialBasico.Cobre, new Precio(0, 0, 2) },
        { TipoMaterialBasico.Madera, new Precio(0, 0, 1) },
        { TipoMaterialBasico.Cuero, new Precio(0, 0, 2) },
        { TipoMaterialBasico.Piedra, new Precio(0, 0, 1) },
        { TipoMaterialBasico.Diamante, new Precio(100, 0, 0) },        
    };

    public static Precio ObtenerPrecio(TipoMaterialBasico tipoMaterialBasico)
    {
        return costes[tipoMaterialBasico];
    }
}

public class MaterialBasico : ObjetoInventario, ICombinable, IComerciable
{   
    private TipoMaterialBasico tipoMaterialBasico;
    private int cantidad;

    public TipoMaterialBasico TipoMaterialBasico
    {
        get => tipoMaterialBasico;
        set => tipoMaterialBasico = value;
    }

    public int Cantidad
    {
        get => cantidad;
        private set => cantidad = Mathf.Max(0, value);
    }
    public bool PuedeCombinarse
    {
        get => true;
    }

    public bool EsComerciable
    {
        get => true;
    }

    public MaterialBasico(TipoMaterialBasico tipoMaterialBasico, GameObject objetoVisual, int cantidad) : base(obtenerNombre(tipoMaterialBasico), obtenerDescripcion(tipoMaterialBasico), obtenerRareza(tipoMaterialBasico), objetoVisual, new Precio(0,0,0))
    {
        Cantidad = cantidad;
        TipoMaterialBasico = tipoMaterialBasico;
        Precio precio = PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico);
        Precio = new Precio(precio.CosteOro * cantidad, precio.CostePlata * cantidad, cantidad * precio.CosteBronce);        
    }   

    private static string obtenerNombre(TipoMaterialBasico tipoMaterialBasico)
    {
        return tipoMaterialBasico switch
        {
            TipoMaterialBasico.Oro => "Oro",
            TipoMaterialBasico.Plata => "Plata",
            TipoMaterialBasico.Bronce => "Bronce",
            TipoMaterialBasico.Hierro => "Hierro",
            TipoMaterialBasico.Acero => "Acero",
            TipoMaterialBasico.Aluminio => "Aluminio",
            TipoMaterialBasico.Cobre => "Cobre",
            TipoMaterialBasico.Madera => "Madera",
            TipoMaterialBasico.Cuero => "Cuero",
            TipoMaterialBasico.Piedra => "Piedra",
            TipoMaterialBasico.Diamante => "Diamante",
            _ => "Material desconocido"
        };
    }

    private static string obtenerDescripcion(TipoMaterialBasico tipoMaterialBasico)
    {
        return tipoMaterialBasico switch
        {
            TipoMaterialBasico.Oro => "Unidad de oro dentro del juego",
            TipoMaterialBasico.Plata => "Unidad de plata dentro del juego",
            TipoMaterialBasico.Bronce => "Unidad de bronce dentro del juego",
            TipoMaterialBasico.Hierro => "Unidad de hierro dentro del juego",
            TipoMaterialBasico.Acero => "Unidad de acero dentro del juego",
            TipoMaterialBasico.Aluminio => "Unidad de aluminio dentro del juego",
            TipoMaterialBasico.Cobre => "Unidad de cobre dentro del juego",
            TipoMaterialBasico.Madera => "Unidad de madera dentro del juegon",
            TipoMaterialBasico.Cuero => "Unidad de cuero dentro del juego",
            TipoMaterialBasico.Piedra => "Unidad de piedra dentro del juego",
            TipoMaterialBasico.Diamante => "Unidad de diamante dentro del juegoo",
            _ => "Material desconocido"
        };
    }

    private static Rareza obtenerRareza(TipoMaterialBasico tipoMaterialBasico)
    {
        return tipoMaterialBasico switch
        {
            TipoMaterialBasico.Oro => Rareza.Epico,
            TipoMaterialBasico.Plata => Rareza.Raro,
            TipoMaterialBasico.Bronce => Rareza.Epico,
            TipoMaterialBasico.Hierro => Rareza.Comun,
            TipoMaterialBasico.Acero => Rareza.Comun,
            TipoMaterialBasico.Aluminio => Rareza.Comun,
            TipoMaterialBasico.Cobre => Rareza.Comun,
            TipoMaterialBasico.Madera => Rareza.Comun,
            TipoMaterialBasico.Cuero => Rareza.Comun,
            TipoMaterialBasico.Piedra => Rareza.Comun,
            TipoMaterialBasico.Diamante => Rareza.Legendario,
            _ => Rareza.Comun
        };
    }
        
    public override string ToString()
    {
        return base.ToString() +
                $"Tipo de material básico: {TipoMaterialBasico}\n" +
                $"Cantidad: {Cantidad}\n";
    }

    public void Combinar(ICombinable objeto) 
    {
        if (EsCombinable(objeto) && objeto is MaterialBasico materialBasico)
        {            
            if (tipoMaterialBasico != materialBasico.TipoMaterialBasico)
            {
                Debug.Log($"Has combinado {Nombre} con {materialBasico.Nombre}.");
            }
            else
            {
                Debug.Log($"No puedes combinar dos materiales del mismo tipo");
            }
        }
        else
        {
            Debug.Log($"La combinación no es posible");
        }
    }
    public bool EsCombinable(ICombinable objeto)
    {
        //aquí deberíamos comprobar si el objeto que nos pasan es combinable con el objeto actual
        //en este caso, como todos los materiales básicos son combinables entre sí, devolvemos true
        return objeto is MaterialBasico;
    }

    public void IncrementarCantidad(int cantidad)
    {
        Cantidad += cantidad;
        ActualizarPrecio();         
    }

    public void DecrementarCantidad(int cantidad)
    {
        Cantidad -= cantidad;
        ActualizarPrecio(); 
    }

    public void ActualizarPrecio()
    {
        Precio.AjustarPrecio(Cantidad * PreciosMaterialesBasicos.ObtenerPrecio(TipoMaterialBasico).CosteOro,
                             Cantidad * PreciosMaterialesBasicos.ObtenerPrecio(TipoMaterialBasico).CostePlata,
                             Cantidad * PreciosMaterialesBasicos.ObtenerPrecio(TipoMaterialBasico).CosteBronce);    
        
    }



    public void Comprar()
    {
        Debug.Log($"Has comprado {Nombre}, cantidad actual: {Cantidad}, valor actual: {Precio.CosteOro}oro, {Precio.CostePlata}plata, {Precio.CosteBronce}bronce");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre}, cantidad actual: {Cantidad}, valor actual: {Precio.CosteOro}oro, {Precio.CostePlata}plata, {Precio.CosteBronce}bronce");
    } 
}
