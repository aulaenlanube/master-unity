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
    public NivelArma Nivel { get; set; }
    public float DPS { get; set; }
    public float VelocidadAtaque { get; set; }
    public int Durabilidad { get; set; }
    public float Alcance { get; set; }

    
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

