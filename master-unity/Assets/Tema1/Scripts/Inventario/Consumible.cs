using UnityEngine;

public enum CategoriaConsumible
{
    Pocion,
    Elixir,
    Comida,
    Bebida,
    Pergamino
}

public class Consumible : ObjetoInventario, IConsumible, IComerciable
{   
    private string efecto;
    private float porcentajeRestante;
    private int duracionEfecto; // duración del efecto en minutos, 0 si no tiene duración
    private bool comerciable;
    private CategoriaConsumible categoriaConsumible;

    public string Efecto
    {
        get => efecto;
        set => efecto = value;
    }   
    
    public int DuracionEfecto
    {
        get => duracionEfecto;
        set => duracionEfecto = Mathf.Clamp(value, 0, 60); // duración máxima de 1 hora
    }

    public float PorcentajeRestante
    {  
        get => porcentajeRestante;
        set => porcentajeRestante = Mathf.Clamp(value, 0, 100);  // porcentaje máximo de 100
    }

    public bool EsComerciable
    {
        get => comerciable;
        private set => comerciable = value;
    }
    
    public CategoriaConsumible CategoriaConsumible
    {
        get => categoriaConsumible;
        set => categoriaConsumible = value;
    }

    public Consumible(string nombre, string descripcion, Rareza rareza, CategoriaConsumible categoriaConsumible, GameObject objetoVisual, Precio precio, string efecto, int duracionEfecto, float porcentajeRestante, bool comerciable)
        : base(nombre, descripcion, rareza, objetoVisual, precio)
    {
        Efecto = efecto;
        DuracionEfecto = duracionEfecto;
        PorcentajeRestante = porcentajeRestante;
        CategoriaConsumible = categoriaConsumible;
        EsComerciable = comerciable;
    }

    public override string ToString()
    {
        return base.ToString() +
                $"Efecto: {Efecto}\n" +
                $"Duración del efecto: {DuracionEfecto}\n" +
                $"Porcentaje restante: {PorcentajeRestante}\n";
    }

    public void Consumir()
    {
        Debug.Log($"Has consumido {Nombre} completamente. {Efecto}" + (duracionEfecto == 0 ? " de forma instantanea." : $" durante {DuracionEfecto}minutos."));
        porcentajeRestante = 0;
    }

    public void Consumir(float porcentaje)
    {
        float porcentajeAjustado = Mathf.Clamp(porcentaje, 0, 100);
        if(porcentajeAjustado > PorcentajeRestante)
        {
            Debug.Log($"Has consumido el {PorcentajeRestante}% restante de {Nombre}.");
        }
        else
        {
            PorcentajeRestante -= porcentajeAjustado;
            Debug.Log($"Has consumido un {porcentajeAjustado}% de {Nombre}. Te queda un {PorcentajeRestante}% de {Nombre}");            
        }
    }

    public void Comprar()
    {
        Debug.Log($"Has comprado {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }
}
