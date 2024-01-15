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

public struct PrecioMaterialBasico
{
    public int CosteOro { get; }
    public int CostePlata { get; }
    public int CosteBronce { get; }

    public PrecioMaterialBasico(int costeOro, int costePlata, int costeBronce)
    {
        CosteOro = costeOro;
        CostePlata = costePlata;
        CosteBronce = costeBronce;
    }
}

public static class PreciosMaterialesBasicos
{
    private static readonly Dictionary<TipoMaterialBasico, PrecioMaterialBasico> costes = new Dictionary<TipoMaterialBasico, PrecioMaterialBasico>
    {
        { TipoMaterialBasico.Oro, new PrecioMaterialBasico(1, 0, 0) },
        { TipoMaterialBasico.Plata, new PrecioMaterialBasico(0, 1, 0) },
        { TipoMaterialBasico.Bronce, new PrecioMaterialBasico(0, 0, 1) },
        { TipoMaterialBasico.Hierro, new PrecioMaterialBasico(0, 0, 1) },
        { TipoMaterialBasico.Acero, new PrecioMaterialBasico(0, 0, 2) },
        { TipoMaterialBasico.Aluminio, new PrecioMaterialBasico(0, 0, 1) },
        { TipoMaterialBasico.Cobre, new PrecioMaterialBasico(0, 0, 2) },
        { TipoMaterialBasico.Madera, new PrecioMaterialBasico(0, 0, 1) },
        { TipoMaterialBasico.Cuero, new PrecioMaterialBasico(0, 0, 2) },
        { TipoMaterialBasico.Piedra, new PrecioMaterialBasico(0, 0, 1) },
        { TipoMaterialBasico.Diamante, new PrecioMaterialBasico(100, 0, 0) },        
    };

    public static PrecioMaterialBasico ObtenerPrecio(TipoMaterialBasico tipoMaterialBasico)
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

    public MaterialBasico(TipoMaterialBasico tipoMaterialBasico, GameObject objetoVisual, int cantidad) : base(obtenerNombre(tipoMaterialBasico), obtenerDescripcion(tipoMaterialBasico), obtenerRareza(tipoMaterialBasico), objetoVisual, 0, 0, 0)
    {
        Cantidad = cantidad;
        TipoMaterialBasico = tipoMaterialBasico;
        PrecioMaterialBasico precio = PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico);
        CosteOro = precio.CosteOro * cantidad;
        CostePlata = precio.CostePlata * cantidad;
        CosteBronce = precio.CosteBronce * cantidad;
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
        if (EsCombinable(objeto))
        {
            MaterialBasico materialBasico = objeto as MaterialBasico;
            if (materialBasico != null && tipoMaterialBasico != materialBasico.TipoMaterialBasico)
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
        CosteOro += PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteOro * cantidad;
        CostePlata += PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CostePlata * cantidad;
        CosteBronce += PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteBronce * cantidad;
    }

    public void DecrementarCantidad(int cantidad)
    {
        Cantidad -= cantidad;
        CosteOro -= PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteOro * cantidad;
        CostePlata -= PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CostePlata * cantidad;
        CosteBronce -= PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteBronce * cantidad;
    }

    public void Comprar()
    {
        Debug.Log($"Has comprado {Nombre}, cantidad actual: {Cantidad}, valor actual: {CosteOro}oro, {CostePlata}plata, {CosteBronce}bronce");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre}, cantidad actual: {Cantidad}, valor actual: {CosteOro}oro, {CostePlata}plata, {CosteBronce}bronce");
    } 
}
