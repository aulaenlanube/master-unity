using UnityEngine;

public enum TipoMonedaShooter
{
    oro = 30,
    plata = 20,
    bronce = 10
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
        Vector3 posicionAleatoria = new Vector3(Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn),
                                                transform.position.y,
                                                Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn));

        // Mueve el objeto a la posición válida
        transform.position = posicionAleatoria;

    }

    private void ActualizarTipoMoneda()
    {
        System.Array valores = System.Enum.GetValues(typeof(TipoMonedaShooter));
        tipoMoneda = (TipoMonedaShooter)valores.GetValue(new System.Random().Next(valores.Length));

        Color color = tipoMoneda switch
        {
            TipoMonedaShooter.oro => MiniShooter.instance.ColorOro,
            TipoMonedaShooter.plata => MiniShooter.instance.ColorPlata,
            TipoMonedaShooter.bronce => MiniShooter.instance.ColorBronce,
            _ => MiniShooter.instance.ColorOro
        };

        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
