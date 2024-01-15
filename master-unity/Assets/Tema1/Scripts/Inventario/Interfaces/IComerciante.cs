public interface IComerciante 
{  
    string Nombre { get; }
    int Oro { get; }
    int Plata { get; }
    int Bronce { get; }

    void Comprar(IComerciable objeto);
    void Vender(IComerciable objeto);
    void ListarInventario();   
}
