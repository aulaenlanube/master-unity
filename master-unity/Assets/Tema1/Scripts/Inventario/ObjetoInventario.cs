using UnityEngine;

public enum Rareza 
{ 
    Comun, 
    Raro, 
    Epico, 
    Legendario
}


public abstract class ObjetoInventario
{
    public string Nombre { get; private set; }
    public string Descripcion { get; set; }
    public Rareza Rareza { get; set; }
    public GameObject ObjetoVisual { get; set; }
    public int CosteOro { get; set; }
    public int CostePlata { get; set; }
    public int CosteBronce { get; set; }
    
    protected ObjetoInventario(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Rareza = rareza;
        ObjetoVisual = objetoVisual;
        CosteOro = costeOro;
        CostePlata = costePlata;
        CosteBronce = costeBronce;        
    }


    public override bool Equals(object obj)
    {
        return obj is ObjetoInventario objInventario && Nombre == objInventario.Nombre;
    }

    public override int GetHashCode()
    {
        return Nombre.GetHashCode();
    }

    public override string ToString()
    {
        return $"Nombre del objeto: {Nombre}\n" +
                $"Rareza: {Rareza}\n" +
                $"Descripción: {Descripcion}\n" +
                $"Coste: {CosteOro}oro, {CostePlata}plata, {CosteBronce}bronce\n";
    }
}

