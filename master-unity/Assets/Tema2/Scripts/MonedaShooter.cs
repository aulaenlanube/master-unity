using UnityEngine;

public enum TipoMonedaShooter
{
    oro,
    plata,
    bronce
}

public class MonedaShooter : MonoBehaviour
{ 
    // variables privadas
    private int ladoZonaRespawn;
    private TipoMonedaShooter tipoMoneda;
    private int puntosMoneda;

    // evento para actualizar la puntuación
    public delegate void recogerMoneda(int puntos);
    public static event recogerMoneda monedaRecogida;

    private void Start()
    {
        ladoZonaRespawn = MiniShooter.instance.LadoZonaRespawn;
        ActualizarTipoMoneda();
    }

    void Update()
    {
        // rotación de la moneda
        transform.Rotate(new Vector3(0, 0, 300f * Time.deltaTime));
    }

    public void RecolectarMoneda()
    {
        monedaRecogida?.Invoke(puntosMoneda);
        MoverMoneda();
        ActualizarTipoMoneda();
    }

    private void MoverMoneda()
    {        
        transform.position = new Vector3(Random.Range(-ladoZonaRespawn, ladoZonaRespawn), 
                                         transform.position.y, 
                                         Random.Range(-ladoZonaRespawn, ladoZonaRespawn));
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

        puntosMoneda = tipoMoneda switch
        {
            TipoMonedaShooter.oro => 3,
            TipoMonedaShooter.plata => 2,
            TipoMonedaShooter.bronce => 1,
            _ => 0
        };
    }

}
