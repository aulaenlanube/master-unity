using UnityEngine;

public enum TipoMonedaShooter
{
    oro = 3,
    plata = 2,
    bronce = 1
}

public class MonedaShooter : MonoBehaviour
{     
    private TipoMonedaShooter tipoMoneda;    

    // evento para actualizar la puntuación
    public delegate void recogerMoneda(int puntos);
    public static event recogerMoneda monedaRecogida;

    private void Start()
    {        
        ActualizarTipoMoneda();
    }

    void Update()
    {
        // rotación de la moneda
        transform.Rotate(new Vector3(0, 0, 300f * Time.deltaTime));
    }

    public void RecolectarMoneda()
    {
        monedaRecogida?.Invoke((int)tipoMoneda);
        MoverMoneda();
        ActualizarTipoMoneda();
    }

    private void MoverMoneda()
    {        
        transform.position = new Vector3(Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn), 
                                         transform.position.y, 
                                         Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn));
    }

    private void ActualizarTipoMoneda()
    {
        System.Array valores = System.Enum.GetValues(typeof(TipoMonedaShooter));
        tipoMoneda = (TipoMonedaShooter)valores.GetValue(new System.Random().Next(valores.Length));

        GetComponent<Renderer>().material.color = tipoMoneda switch
        {
            TipoMonedaShooter.oro => MiniShooter.instance.ColorOro,
            TipoMonedaShooter.plata => MiniShooter.instance.ColorPlata,
            TipoMonedaShooter.bronce => MiniShooter.instance.ColorBronce,
            _ => MiniShooter.instance.ColorOro
        } ;
    }
}
