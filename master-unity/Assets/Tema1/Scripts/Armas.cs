using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class Posicion 
{
    public int x, y, z;
    public Posicion(int x, int y, int z) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return $"{x}:{y}:{z}";
    }
}
public enum TipoArma
{
    Espada,
    Arco,
    Lanza
}

public struct DatosArma
{
    public string nombre;
    public int da�o;
    public TipoArma tipo;

    public DatosArma(string nombre, int da�o, TipoArma tipo)
    {
        this.nombre = nombre;
        this.da�o = da�o;
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return $"Nombre del arma: {nombre}, Da�o: {da�o}, Tipo: {tipo}";
    }
}

public class Armas : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        Vector3 miPosicion1 = Vector3.zero;
        Posicion miPosicion2 = new Posicion(0, 0, 0);
        ModificarObjeto(miPosicion1);
        ModificarObjeto(miPosicion2);

        Debug.Log($"Pos en Start: {miPosicion1}");
        Debug.Log($"Pos en Start: {miPosicion2}");

        TipoArma miArma = TipoArma.Espada;
        Debug.Log("Arma antes: " + miArma);         // Arma antes: Espada

        CambiarTipoArma(miArma);                    // Arma actual: Arco
        Debug.Log("Arma despu�s: " + miArma);	    // Arma despu�s: Espada

        DatosArma arma1 = new DatosArma("Cimitarra de la ira", 28, TipoArma.Espada);

        CambiarDatosArma(arma1);                    //Cimitarra de la ira X 2:56
        Debug.Log($"{arma1.nombre}:{arma1.da�o}"); 	//Cimitarra de la ira:28
    }

    public void CambiarTipoArma(TipoArma armaActual)
    {
        armaActual = armaActual switch
        {
            TipoArma.Espada => TipoArma.Arco,
            TipoArma.Arco   => TipoArma.Lanza,
            TipoArma.Lanza  => TipoArma.Espada,
            _               => TipoArma.Espada
        };
        Debug.Log("Arma actual: " + armaActual);
    }

    public void CambiarDatosArma(DatosArma datos)
    {
        datos.da�o *= 2;
        datos.nombre += " X 2";
        Debug.Log($"{datos.nombre}:{datos.da�o}");
    }

    public void ModificarObjeto(Vector3 pos)
    {
        pos.x += 10;
        Debug.Log($"Posici�n en el m�todo ModificarObjeto:{pos}");
    }
    public void ModificarObjeto(Posicion pos)
    {
        pos.x += 10;
        Debug.Log($"Posici�n en el m�todo ModificarObjeto:{pos}");
    }



}
