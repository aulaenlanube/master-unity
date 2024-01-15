// permite combinar materiales basicos para obtener materiales de mayor rareza, armas y armaduras
public interface ICombinable 
{    
    bool PuedeCombinarse { get; }

    void Combinar(ICombinable objeto);  
    
    bool EsCombinable(ICombinable objeto);

}
