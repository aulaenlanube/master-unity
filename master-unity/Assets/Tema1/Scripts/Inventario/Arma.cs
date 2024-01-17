using UnityEngine;

public enum CategoriaArma
{  
    Espada,
    Arco,
    Lanza,
    Hacha,
    Martillo,
    Daga,
    Baston,
    Varita,
}

public class Arma : ObjetoInventario, IMejorable, ICombinable, IComerciable, IInteractuable
{    
    private float dps;
    private float velocidadAtaque;
    private int durabilidad;
    private float alcance;
    private bool combinable;
    private bool comerciable;
    private NivelMejora nivelMejora;

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
    public bool PuedeCombinarse
    { 
        get => combinable;
        private set => combinable = value; 
    }

    public NivelMejora NivelMejora
    {
        get => nivelMejora;
        private set => nivelMejora = value;
    }

    public bool EsComerciable
    {
        get => comerciable;
        private set => comerciable = value;
    }

    public Arma(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, float dps, float velocidadAtaque, int durabilidad, float alcance, bool combinable, bool comerciable)
            : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {      
        DPS = dps;
        VelocidadAtaque = velocidadAtaque;
        Durabilidad = durabilidad;
        Alcance = alcance;
        PuedeCombinarse = combinable;
        NivelMejora = NivelMejora.Basico;
        EsComerciable = comerciable;
    }

    public override string ToString()
    {
        return base.ToString()  +
                $"DPS: {DPS}\n" +
                $"Velocidad de ataque: {VelocidadAtaque}\n" +
                $"Durabilidad: {Durabilidad}\n" +
                $"Alcance: {Alcance}\n";
    }

    public void Mejorar()
    {
        switch(NivelMejora)
        {
            case NivelMejora.Basico:
                DPS += 20;
                VelocidadAtaque += 20;
                Durabilidad += 20;
                NivelMejora = NivelMejora.Avanzado;
                break;
            case NivelMejora.Intermedio:
                DPS += 30;
                VelocidadAtaque += 30;
                Durabilidad += 30;
                NivelMejora = NivelMejora.Avanzado;
                break;
            case NivelMejora.Avanzado:
                DPS += 50;
                VelocidadAtaque += 50;
                Durabilidad += 50;
                NivelMejora = NivelMejora.Maestro;
                break;
            case NivelMejora.Maestro:
                Debug.Log("El arma ya está al máximo nivel.");
                break;
        }        
    }

    public void Comprar()
    {
        Debug.Log($"Has comprado {Nombre} por {CosteOro} de oro, {CostePlata} de plata y {CosteBronce} de bronce.");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre} por {CosteOro} de oro, {CostePlata} de plata y {CosteBronce} de bronce.");
    }   

    public void Combinar(ICombinable objeto)
    {
        if (EsCombinable(objeto))
        {
            Arma arma = objeto as Arma;
            Debug.Log($"Has combinado {Nombre} con {arma.Nombre}.");           
        }
        else
        {
            Debug.Log($"La combinación no es posible");
        }
    }

    public bool EsCombinable(ICombinable objeto)
    {
        //caso general: si el objeto que nos pasan es un Arma y puede combinarse y el objeto actual también, devolvemos true
        if(PuedeCombinarse)
        {
            return objeto is Arma arma && arma.PuedeCombinarse;
        }
        return false;        
    }

    public void Equipar()
    {
        Debug.Log($"Has equipado {Nombre}.");
    }

    public void Desequipar()
    {
        Debug.Log($"Has desequipado {Nombre}.");
    }
}

