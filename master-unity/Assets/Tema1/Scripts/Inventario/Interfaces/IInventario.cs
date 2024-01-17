public interface IInventario
{   
    void AgregarObjeto(ObjetoInventario objeto);
    void EliminarObjeto(ObjetoInventario objeto);
    void MostrarInventario();
    void MostrarInventarioPorValor();
    void MostrarInventarioPorRareza(Rareza rareza);
}
