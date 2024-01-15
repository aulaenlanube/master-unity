using UnityEngine;

public enum ParteArmadura
{
    Cabeza,
    Central,
    Brazos,
    Manos,
    Piernas,
    Pies,
    Escudo
}

public class Armadura : ObjetoInventario, IMejorable, ICombinable, IComerciable, IInteractuable
{
    private int defensa;
    private int agilidad;
    private int durabilidad;
    private bool combinable;
    private bool comerciable;
    private NivelMejora nivelMejora;

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


    public Armadura(string nombre, string descripcion, Rareza rareza, GameObject objetoVisual, int costeOro, int costePlata, int costeBronce, int defensa, int agilidad, int durabilidad, ParteArmadura parte, bool combinable, bool comerciable)
        : base(nombre, descripcion, rareza, objetoVisual, costeOro, costePlata, costeBronce)
    {
        Defensa = defensa;
        Agilidad = agilidad;
        Durabilidad = durabilidad;
        Parte = parte;
        PuedeCombinarse = combinable;
        NivelMejora = NivelMejora.Basico;
        EsComerciable = comerciable;
    }


    public override string ToString()
    {
        return base.ToString() +
                $"Defensa: {Defensa}\n" +
                $"Agilidad: {Agilidad}\n" +
                $"Durabilidad: {Durabilidad}\n" +
                $"Parte: {Parte}\n";
    }
    public void Mejorar()
    {
        switch (NivelMejora)
        {
            case NivelMejora.Basico:
                Defensa += 10;
                Agilidad += 10;
                Durabilidad += 10;
                NivelMejora = NivelMejora.Avanzado;
                break;
            case NivelMejora.Intermedio:
                Defensa += 20;
                Agilidad += 20;
                Durabilidad += 20;
                NivelMejora = NivelMejora.Avanzado;
                break;
            case NivelMejora.Avanzado:
                Defensa += 30;
                Agilidad += 30;
                Durabilidad += 30;
                NivelMejora = NivelMejora.Maestro;
                break;
            case NivelMejora.Maestro:
                Debug.Log("La armadura ya está al máximo nivel.");
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
            Armadura armadura = objeto as Armadura;
            Debug.Log($"Has combinado {Nombre} con {armadura.Nombre}.");
        }
        else
        {
            Debug.Log($"La combinación no es posible");
        }
    }

    public bool EsCombinable(ICombinable objeto)
    {
        //caso general: si el objeto que nos pasan es un Armadura y puede combinarse y el objeto actual también, devolvemos true
        if (PuedeCombinarse)
        {
            return objeto is Armadura arma && arma.PuedeCombinarse;
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
