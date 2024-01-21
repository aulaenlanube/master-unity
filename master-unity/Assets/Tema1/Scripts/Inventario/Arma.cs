using UnityEngine;

public enum CategoriaArma
{
    Espada,
    Arco,
    Ballesta,
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
    private CategoriaArma categoriaArma;
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
    public CategoriaArma CategoriaArma
    {
        get => categoriaArma;
        set => categoriaArma = value;
    }

    public bool EsComerciable
    {
        get => comerciable;
        private set => comerciable = value;
    }

    public Arma(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, Precio precio, float dps, float velocidadAtaque, int durabilidad, float alcance, bool combinable, bool comerciable, CategoriaArma categoriaArma)
            : base(nombre, descripcion, rareza, objetoVisual, precio)
    {
        DPS = dps;
        VelocidadAtaque = velocidadAtaque;
        Durabilidad = durabilidad;
        Alcance = alcance;
        PuedeCombinarse = combinable;
        NivelMejora = NivelMejora.Basico;
        EsComerciable = comerciable;
        CategoriaArma = categoriaArma;        
    }

    public override string ToString()
    {
        return base.ToString() +
                $"DPS: {DPS}\n" +
                $"Velocidad de ataque: {VelocidadAtaque}\n" +
                $"Durabilidad: {Durabilidad}\n" +
                $"Alcance: {Alcance}\n";
    }

    public void Mejorar()
    {
        switch (NivelMejora)
        {
            case NivelMejora.Basico:
                DPS += 20;
                VelocidadAtaque += 20;
                Durabilidad += 20;
                NivelMejora = NivelMejora.Intermedio;
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
        Debug.Log($"Has comprado {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }

    public void Vender()
    {
        Debug.Log($"Has vendido {Nombre} por {Precio.CosteOro} de oro, {Precio.CostePlata} de plata y {Precio.CosteBronce} de bronce.");
    }

    public void Combinar(ICombinable objeto)
    {
        //si ambos con combinables y recibimos un Arma
        if (EsCombinable(objeto) && objeto is Arma arma)
        {
            Debug.Log($"Has combinado {Nombre} con {arma.Nombre}.");
        }
        else
        {
            Debug.Log($"La combinación no es posible");
        }
    }

    public bool EsCombinable(ICombinable objeto)
    {
        //caso general: si el objeto puede combinarse y el objeto actual también, devolvemos true
        return PuedeCombinarse && objeto.PuedeCombinarse;
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

