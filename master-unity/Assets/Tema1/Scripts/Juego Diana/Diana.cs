using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diana : MonoBehaviour
{
    public float velocidad = 20;
    public int cantidadDisparos = 20;
    public float duracionPartida = 20;

    private int puntuacionActual;
    private Vector3[][] posiciones;
    private Coroutine corrutinaMovimientoActual;
    private float duracionActual;

    public delegate void textoPuntuacion(int puntuacion);
    public static event textoPuntuacion puntuacionActualizada;

    public delegate void textoDisparos(int disparos);
    public static event textoDisparos disparosActualizados;

    public delegate void textoTiempo(float duracion);
    public static event textoTiempo tiempoActualizado;
    
    public static event textoPuntuacion partidaFinalizada;

    void Start()
    {
        puntuacionActual = 0;
        duracionActual = 0;

        //actualizar los marcadores
        puntuacionActualizada?.Invoke(puntuacionActual);
        disparosActualizados?.Invoke(cantidadDisparos);
        tiempoActualizado?.Invoke(duracionPartida - duracionActual);

        EstablecerPatronesMovimiento();
        PatronMovimientoAleatorio();
    }

    void Update()
    {      
        duracionActual += Time.deltaTime;
        tiempoActualizado?.Invoke(duracionPartida - duracionActual);

        if (Input.GetMouseButtonDown(0))
        {            
            cantidadDisparos--;
            disparosActualizados?.Invoke(cantidadDisparos);

            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) // si hay algún impacto
            {
                //establemos el impacto y se actualiza la puntuación
                int puntosImpacto = hit.collider.GetComponent<PuntuacionDiana>()?.puntosPorImpacto ?? 0;
                puntuacionActual += puntosImpacto;
                                         
                if (puntosImpacto > 0)
                {
                    PatronMovimientoAleatorio();     //cambiamos el patrón de movimiento
                    puntuacionActualizada?.Invoke(puntuacionActual);
                }
                if (puntosImpacto == 25) StartCoroutine(GirarDiana());  //giramos la diana si hemos dado en el centro
            }
        }        

        //si no quedan disparos o no queda tiempo, la partida finaliza
        if (cantidadDisparos == 0 || duracionActual >= duracionPartida)
        {
            tiempoActualizado?.Invoke(0); //actualizamos el tiempo
            partidaFinalizada?.Invoke(puntuacionActual); //guardamos la puntuación y mostramos las puntuaciones
           
            //destruimos la diana
            Destroy(gameObject);
        }
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

