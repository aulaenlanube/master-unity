using System.Collections.Generic;

public interface IInventario
{
    List<ObjetoInventario> Objetos { get; }
    void AgregarObjeto(ObjetoInventario objeto);
    void EliminarObjeto(ObjetoInventario objeto);
    void MostrarInventario();
    void MostrarInventarioPorValor();
    void MostrarInventarioPorRareza(Rareza rareza);
}
