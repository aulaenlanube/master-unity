using UnityEngine;

public enum ParteArmadura
{
    Cabeza,
    Central,
    Brazos,
    Manos,
    Piernas,
    Pies
}

public class Armadura : ObjetoInventario
{
    private int defensa;
    private int agilidad;
    private int durabilidad;

    public int Defensa
    {
        get => defensa;
        set => defensa = Mathf.Max(0, value); 
    }
    public int Agilidad
    {
        get => agilidad;
        set => agilidad = Mathf.Max(0, value); 
    }
    public int Durabilidad
    {
        get => durabilidad;
        set => durabilidad = Mathf.Max(0, value); 
    }

    public ParteArmadura Parte { get; set; }

    public Armadura(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, int defensa, int agilidad, int durabilidad, ParteArmadura parte)
        : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        Defensa = defensa;
        Agilidad = agilidad;
        Durabilidad = durabilidad;
        Parte = parte;
    }


    public override string ToString()
    {
        return base.ToString() +
                $"Defensa: {Defensa}\n" +
                $"Agilidad: {Agilidad}\n" +
                $"Durabilidad: {Durabilidad}\n" +
                $"Parte: {Parte}\n";
    }
}
