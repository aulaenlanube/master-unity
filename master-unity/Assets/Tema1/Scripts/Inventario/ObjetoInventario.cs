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
    private string nombre;
    private string descripcion;
    private Rareza rareza;
    private GameObject objetoVisual;
    private int costeOro;
    private int costePlata;
    private int costeBronce;

    public string Nombre
    {
        get => nombre;
        set => nombre = value;
    }

    public string Descripcion
    {
        get => descripcion;
        set => descripcion = value;
    }

    public Rareza Rareza
    {
        get => rareza;
        set => rareza = value;
    }

    public GameObject ObjetoVisual
    {
        get => objetoVisual;
        set => objetoVisual = value;
    }

    public int CosteOro
    {
        get => costeOro;
        set => costeOro = Mathf.Max(0, value);
    }

    public int CostePlata
    {
        get => costePlata;
        set => costePlata = Mathf.Max(0, value);
    }

    public int CosteBronce
    {
        get => costeBronce;
        set => costeBronce = Mathf.Max(0, value);
    }

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
        return  $"Nombre del objeto: {Nombre}\n" +
                $"Rareza: {Rareza}\n" +
                $"Descripción: {Descripcion}\n" +
                $"Coste: {CosteOro}oro, {CostePlata}plata, {CosteBronce}bronce\n";
    }
}

