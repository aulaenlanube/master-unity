//en principio servirá para mejorar armas y armaduras
public enum NivelMejora
{
    Basico,
    Intermedio,
    Avanzado,
    Maestro
}

public interface IMejorable
{    
    NivelMejora NivelMejora { get; }
    void Mejorar();    
}
