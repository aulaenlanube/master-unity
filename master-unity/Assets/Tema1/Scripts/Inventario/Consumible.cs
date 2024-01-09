using UnityEngine;

public class Consumible : ObjetoInventario
{   
    private string efecto;
    private float porcentajeRestante;
    private float duracionEfecto;

    public string Efecto
    {
        get => efecto;
        set => efecto = value;
    }   
    
    public float DuracionEfecto
    {
        get => duracionEfecto;
        set => duracionEfecto = Mathf.Clamp(value, 0, 500); 
    }

    public float PorcentajeRestante
    {  
        get => porcentajeRestante;
        set => porcentajeRestante = Mathf.Clamp(value, 0, 100);  
    }

    public Consumible(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, string efecto, float duracionEfecto, float porcentajeRestante)
        : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        Efecto = efecto;
        DuracionEfecto = duracionEfecto;
        PorcentajeRestante = porcentajeRestante;
    }

    public override string ToString()
    {
        return base.ToString() +
                $"Efecto: {Efecto}\n" +
                $"Duración del efecto: {DuracionEfecto}\n" +
                $"Porcentaje restante: {PorcentajeRestante}\n";
    }
}
