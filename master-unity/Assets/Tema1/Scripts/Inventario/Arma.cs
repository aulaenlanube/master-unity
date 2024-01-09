using UnityEngine;

public enum NivelArma 
{ 
    Inicial, 
    Avanzado, 
    Elite, 
    Leyenda 
}

public class Arma : ObjetoInventario
{
    private NivelArma nivel;
    private float dps;
    private float velocidadAtaque;
    private int durabilidad;
    private float alcance;

    public NivelArma Nivel
    {
        get => nivel;
        set => nivel = value;
    }
    public float DPS
    {
        get => dps;
        set => dps = Mathf.Max(0, value); 
    }
    public float VelocidadAtaque
    {
        get => velocidadAtaque;
        set => velocidadAtaque = Mathf.Max(0, value); 
    }
    public int Durabilidad
    {
        get => durabilidad;
        set => durabilidad = Mathf.Max(0, value); 
    }
    public float Alcance
    {
        get => alcance;
        set => alcance = Mathf.Max(0, value); 
    }
    
    public Arma(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, NivelArma nivel, float dps, float velocidadAtaque, int durabilidad, float alcance)
            : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        Nivel = nivel;
        DPS = dps;
        VelocidadAtaque = velocidadAtaque;
        Durabilidad = durabilidad;
        Alcance = alcance;
    }

    public override string ToString()
    {
        return base.ToString() +
                $"Nivel: {Nivel}\n" +
                $"DPS: {DPS}\n" +
                $"Velocidad de ataque: {VelocidadAtaque}\n" +
                $"Durabilidad: {Durabilidad}\n" +
                $"Alcance: {Alcance}\n";
    }
}

