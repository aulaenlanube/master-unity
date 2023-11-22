using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Diana : MonoBehaviour
{
    public float velocidad = 20;
    public int cantidadDisparos = 20;
    public float duracionPartida = 20;
    public Text textoPuntuacionActual;
    public Text textoDisparosRestantes;
    public Text textoTiempoRestante;

    private int puntuacionActual;
    private Vector3[][] posiciones;
    private Coroutine corrutinaMovimientoActual;
    private float duracionActual;

    void Start()
    {
        puntuacionActual = 0;
        duracionActual = 0;

        ActualizarTextos();
        EstablecerPatronesMovimiento();
        PatronMovimientoAleatorio();
    }

    void Update()
    {        
        duracionActual += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {            
            cantidadDisparos--;
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) // si hay algún impacto
            {
                //establemos el impacto y se actualiza la puntuación
                int puntoImpaco = hit.collider.GetComponent<PuntuacionDiana>().puntosPorImpacto;
                puntuacionActual += puntoImpaco;
                                         
                PatronMovimientoAleatorio(); //cambiamos el patrón de movimiento                  
                if (puntoImpaco == 25) StartCoroutine(GirarDiana());//giramos la diana si hemos dado en el centro
            }
        }        
        ActualizarTextos(); 
        if (cantidadDisparos == 0 || duracionActual >= duracionPartida)
        {
            textoTiempoRestante.text = "Fin de la partida";
            Destroy(gameObject);
        }
    }

    private void ActualizarTextos()
    {
        textoDisparosRestantes.text = $"Disparos restantes: {cantidadDisparos}";
        textoPuntuacionActual.text = $"Puntuación: {puntuacionActual}";
        textoTiempoRestante.text = $"Tiempo restante: {(duracionPartida - duracionActual):F2}";
    }

    private void EstablecerPatronesMovimiento()
    {
        posiciones = new Vector3[5][];
        posiciones[0] = new Vector3[] { new Vector3(10, 0, 0), new Vector3(-10, 0, 0) }; // patrón izquierda/derecha
        posiciones[1] = new Vector3[] { new Vector3(0, 5, 0), new Vector3(0, -5, 0) }; // patrón arriba/abajo
        posiciones[2] = new Vector3[] { new Vector3(5, 0, 5), new Vector3(5, 0, -5), new Vector3(-5, 0, 5), new Vector3(-5, 0, -5) }; // zig-zag profundidad
        posiciones[3] = new Vector3[] { new Vector3(10, 0, 0), new Vector3(-10, 0, 0), new Vector3(0, 0, 10) }; // triángulo
        posiciones[4] = new Vector3[] { new Vector3(10, 0, 0), new Vector3(10, 0, 20), new Vector3(-10, 0, 20), new Vector3(-10, 0, 0) }; // cuadrado  
    }

    private void PatronMovimientoAleatorio()
    {
        if(corrutinaMovimientoActual != null) StopCoroutine(corrutinaMovimientoActual);
        corrutinaMovimientoActual = StartCoroutine(MoverEntrePuntos(posiciones[Random.Range(0, posiciones.Length)]));
    }

    IEnumerator MoverEntrePuntos(Vector3[] puntos)
    {
        if (puntos.Length < 2) yield break;
        int indiceSiguientePunto = 0;
        Vector3 inicio = transform.position;

        while (true)
        {
            Vector3 destino = puntos[indiceSiguientePunto];
            transform.position = Vector3.MoveTowards(inicio, destino, velocidad * Time.deltaTime);
            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                inicio = destino;
                indiceSiguientePunto = ++indiceSiguientePunto % puntos.Length;
            }
            else inicio = transform.position;
            yield return null;
        }
    }

    IEnumerator GirarDiana()
    {
        for (int i = 0; i < 18; i++)
        {
            transform.Rotate(0f, 100f, 0f);
            yield return null;
        }
    }
}

