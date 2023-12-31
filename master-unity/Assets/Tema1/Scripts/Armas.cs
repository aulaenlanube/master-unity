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
    public int poder;
    public TipoArma tipo;

    public DatosArma(string nombre, int poder, TipoArma tipo)
    {
        this.nombre = nombre;
        this.poder = poder;
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return $"Nombre del arma: {nombre}, poder: {poder}, Tipo: {tipo}";
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
        Debug.Log("Arma despu?s: " + miArma);	    // Arma despu?s: Espada

        DatosArma arma1 = new DatosArma("Cimitarra de la ira", 28, TipoArma.Espada);

        CambiarDatosArma(arma1);                    //Cimitarra de la ira X 2:56
        Debug.Log($"{arma1.nombre}:{arma1.poder}"); 	//Cimitarra de la ira:28

        (int p, string n, Vector3 v) = ObtenerDatosJugador();
        Debug.Log($"{p}:{n}:{v}");  // 100:aula en la nube:(0.0, 0.0, 0.0)
        (p, n, v) = ObtenerDatosJugador(p, n, v);
        Debug.Log($"{p}:{n}:{v}");      // 5:aulaenlanube.com:(0.0, 0.0, 0.0)
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
        datos.poder *= 2;
        datos.nombre += " X 2";
        Debug.Log($"{datos.nombre}:{datos.poder}");
    }

    public void ModificarObjeto(Vector3 pos)
    {
        pos.x += 10;
        Debug.Log($"Posici?n en el m?todo ModificarObjeto:{pos}");
    }
    public void ModificarObjeto(Posicion pos)
    {
        pos.x += 10;
        Debug.Log($"Posici?n en el m?todo ModificarObjeto:{pos}");
    }

    public (int, string, Vector3) ObtenerDatosJugador()
    {
        int puntuacion = 100;
        string nombre = "aula en la nube";
        Vector3 posicion = Vector3.zero;
        return (puntuacion, nombre, posicion);
    }
    public (int, string, Vector3) ObtenerDatosJugador(int p, string n, Vector3 v)
    {
        p = 5;
        n = "aulaenlanube.com";
        v = Vector3.zero;
        return (p, n, v);
    }




}
