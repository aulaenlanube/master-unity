using UnityEngine;

public enum TipoMoneda
{
    oro,
    plata,
    bronce
}

public class Moneda : MonoBehaviour
{
    private static bool partidaFinalizada = false;
    private static int puntuacionMaxima = 100;
    private static int cantidadMonedas = 1;

    public float velocidad = 300f;
    public Transform cubo1;
    public Transform cubo2;
    public float distanciaParaRecoger = 1f;
    public float distanciaMaximaMoneda = 50f;
    public int cantidadMaximaMonedas = 5;

    [Range(0, 100)]
    public int probabilidadNuevaMoneda;
    public Color colorOro;
    public Color colorPlata;
    public Color colorBronce;

    private TipoMoneda tipoMoneda;
    private int puntosMoneda;

    private void Start()
    {
        ActualizarTipoMoneda();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, velocidad * Time.deltaTime));
        ComprobarCogerMoneda(cubo1.gameObject);
        ComprobarCogerMoneda(cubo2.gameObject);
        if (partidaFinalizada) Destroy(gameObject);
    }

    private void ComprobarCogerMoneda(GameObject cubo)
    {
        if (Vector3.Distance(transform.position, cubo.transform.position) < distanciaParaRecoger)
        {
            cubo.GetComponent<PuntuacionCubo>().AumentarPuntuacion(puntosMoneda);
            if (tipoMoneda == TipoMoneda.oro) Ralentizar(cubo);                
            MoverMoneda();
            ActualizarTipoMoneda();
        }
    }

    private void Ralentizar(GameObject cubo)
    {
        cubo.GetComponent<MoverConFlechasPersonalizable>().RalentizarCubo();
    }
    private void MoverMoneda()
    {
        Vector3 posicionAleatoria = new Vector3(Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda - 1), 0, Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda - 1));
        transform.position = posicionAleatoria;

        //duplicar moneda
        if (Random.Range(0, 100) < probabilidadNuevaMoneda && cantidadMonedas < cantidadMaximaMonedas)
        {
            posicionAleatoria = new Vector3(Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda + 1), 0, Random.Range(-distanciaMaximaMoneda, distanciaMaximaMoneda + 1));
            Quaternion rotacionInicial = Quaternion.Euler(90, 0, 0);
            Instantiate(gameObject, posicionAleatoria, rotacionInicial);
            cantidadMonedas++;
        }
    }

    private void ActualizarTipoMoneda()
    {
        System.Array valores = System.Enum.GetValues(typeof(TipoMoneda));
        tipoMoneda = (TipoMoneda)valores.GetValue(new System.Random().Next(valores.Length));

        GetComponent<Renderer>().material.color = tipoMoneda switch
        {
            TipoMoneda.oro => colorOro,
            TipoMoneda.plata => colorPlata,
            TipoMoneda.bronce => colorBronce,
            _ => colorOro
        };

        puntosMoneda = tipoMoneda switch
        {
            TipoMoneda.oro => 5,
            TipoMoneda.plata => 3,
            TipoMoneda.bronce => 1,
            _ => 0
        };
    }

    public static void ComprobarPartidaFinalizada(int puntos)
    {
        if (puntos >= puntuacionMaxima) partidaFinalizada = true;
    }
}
