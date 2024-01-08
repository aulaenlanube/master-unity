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
    public int Defensa { get; set; }
    public int Agilidad { get; set; }
    public int Durabilidad { get; set; }
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
