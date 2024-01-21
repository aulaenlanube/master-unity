using System;
using UnityEngine;

public enum Rareza
{
    Comun,
    Raro,
    Epico,
    Legendario
}

public struct Precio
{
    private int costeOro { get; set; }
    private int costePlata { get; set; }
    private int costeBronce { get; set;  }

    public Precio(int costeOro, int costePlata, int costeBronce)
    {
        this.costeOro = Mathf.Max(0, costeOro);
        this.costePlata = Mathf.Max(0, costePlata);
        this.costeBronce = Mathf.Max(0, costeBronce);
    }

    public int CosteOro
    {
        get => costeOro;
        private set => costeOro = Mathf.Max(0, value);
    }

    public int CostePlata
    {
        get => costePlata;
        private set => costePlata = Mathf.Max(0, value);        
    }

    public int CosteBronce
    {
        get => costeBronce;
        private set => costeBronce = Mathf.Max(0, value);
    }

    public void AjustarPrecio(int oro, int plata, int bronce)
    {
        CosteOro = oro;
        CostePlata = plata;
        CosteBronce = bronce;        
    }   
}

public abstract class ObjetoInventario
{
    private string id;
    private string nombre;
    private string descripcion;
    private Rareza rareza;
    private GameObject objetoVisual;
    private Precio precio;       

    public string Id
    {
        get => id;
        private set => id = value;
    }

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
    public Precio Precio
    {
        get => precio;
        set => precio = value;
    }

    protected ObjetoInventario(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, Precio precio)
    {
        Id = Guid.NewGuid().ToString();
        Nombre = nombre;
        Descripcion = descripcion;
        Rareza = rareza;
        ObjetoVisual = objetoVisual;
        Precio = precio;
    }

    public override string ToString()
    {
        return $"Nombre del objeto: {Nombre}\n" +
                $"Rareza: {Rareza}\n" +
                $"Descripción: {Descripcion}\n" +
                $"Coste: {Precio.CosteOro}oro, {Precio.CostePlata}plata, {Precio.CosteBronce}bronce\n";
    }
}

