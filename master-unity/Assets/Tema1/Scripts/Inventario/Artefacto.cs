using UnityEngine;

public class Artefacto : ObjetoInventario, IComerciable
{
    public string PropiedadEspecial { get; set; }
    private bool comerciable;

    public bool EsComerciable
    {
        get => comerciable;
        private set => comerciable = value;
    }

    public Artefacto(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, string propiedadEspecial, bool comerciable)
        : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        PropiedadEspecial = propiedadEspecial;
        EsComerciable = comerciable;
    }

    public override string ToString()
    {
        return base.ToString() + $"Propiedad especial: {PropiedadEspecial}\n";
    }

    public void Comprar()
    {
        Debug.Log($"Has comprado {Nombre} por {CosteOro} de oro, {CostePlata} de plata y {CosteBronce} de bronce.");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre} por {CosteOro} de oro, {CostePlata} de plata y {CosteBronce} de bronce.");
    }

}





