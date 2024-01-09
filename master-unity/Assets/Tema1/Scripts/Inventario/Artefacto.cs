using UnityEngine;

public class Artefacto : ObjetoInventario
{
    public string PropiedadEspecial { get; set; }

    public Artefacto(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, string propiedadEspecial)
        : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        PropiedadEspecial = propiedadEspecial;
    }

    public override string ToString()
    {
        return base.ToString() + $"Propiedad especial: {PropiedadEspecial}\n";
    }
}





