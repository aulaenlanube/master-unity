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

    public Artefacto(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, Precio precio, string propiedadEspecial, bool comerciable)
        : base(nombre, descripcion, rareza, objetoVisual, precio)
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
        Debug.Log($"Has comprado {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }

}





