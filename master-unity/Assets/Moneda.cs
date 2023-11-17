using UnityEngine;

public enum TipoMoneda
{
    oro,
    plata,
    bronce
}

public class Moneda : MonoBehaviour
{    
    private static int puntuacionMaxima = 30;
    private static bool juegoFinalizado = false;
    private static int cantidadMonedas = 1;
    private TipoMoneda tipoMoneda;
    private int puntosMoneda;

    public float velocidad = 300f;
    public Transform cubo1;
    public Transform cubo2;
    public float distanciaParaRecoger = 1f;
    public float distanciaMaximaMoneda = 10f;
    public int cantidadMaximaMonedas = 5;

    [Range(0,100)]
    public int probabilidadNuevaMoneda;

    private void Start()
    {
        ActualizarTipoMoneda();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, velocidad*Time.deltaTime));

        if(Vector3.Distance(transform.position, cubo1.position) < distanciaParaRecoger)
        {
            if (tipoMoneda == TipoMoneda.oro) cubo1.GetComponent<MoverConFlechasPersonalizable>().RalentizarCubo();
            cubo1.GetComponent<PuntuacionCubo>().AumentarPuntuacion(puntosMoneda);
            MoverMoneda();
            ActualizarTipoMoneda();
        }

        if (Vector3.Distance(transform.position, cubo2.position) < distanciaParaRecoger)
        {
            if (tipoMoneda == TipoMoneda.oro) cubo2.GetComponent<MoverConFlechasPersonalizable>().RalentizarCubo();
            cubo2.GetComponent<PuntuacionCubo>().AumentarPuntuacion(puntosMoneda);
            MoverMoneda();
            ActualizarTipoMoneda();
        }
        if(juegoFinalizado) Destroy(gameObject);
    }

    private void MoverMoneda()
    {
        Vector3 posicionAleatoria = new Vector3(Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda-1), 0, Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda-1));
        transform.position = posicionAleatoria;

        if(Random.Range(0,100) < probabilidadNuevaMoneda && cantidadMonedas < cantidadMaximaMonedas)
        {
            posicionAleatoria = new Vector3(Random.Range(-10, 11), 0, Random.Range(-10, 11));
            Quaternion rotacionInicial = Quaternion.Euler(90,0,0);
            Instantiate(gameObject, posicionAleatoria, rotacionInicial);
            cantidadMonedas++;
        }
    }

    private void ActualizarTipoMoneda()
    {
        System.Array valores = System.Enum.GetValues(typeof(TipoMoneda));
        tipoMoneda = (TipoMoneda)valores.GetValue(new System.Random().Next(valores.Length));

        Color colorMoneda = tipoMoneda switch
        {
            TipoMoneda.oro => Color.yellow,
            TipoMoneda.plata => Color.grey,
            TipoMoneda.bronce => new Color(0.6f, 0.3f, 0.1f),
            _ => Color.black
        };

        GetComponent<Renderer>().material.color = colorMoneda;

        puntosMoneda = tipoMoneda switch
        {
            TipoMoneda.oro => 5,
            TipoMoneda.plata => 3,
            TipoMoneda.bronce => 1,
            _ => 0
        };
    }

    public static void ComprobarPuntuacionMaxima(int cantidadPuntos)
    {
        if (cantidadPuntos >= puntuacionMaxima) juegoFinalizado = true;
    }

}
