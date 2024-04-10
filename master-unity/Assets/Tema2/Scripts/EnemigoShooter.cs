using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EstadosBarraSalud
{
    verde,
    amarillo,
    rojo
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoShooter : MonoBehaviour
{
    [SerializeField] private GameObject barraDeSalud;
    [SerializeField] private float saludMaxima = 10f;  
    [Range(0, 10)][SerializeField] private float regeneracion = 1f;

    private Vector3[] puntosRuta;
    private int puntosEnemigo = 0;
    private int puntoRutaActual = 0;
    private float saludActual;
    private Vector3 escalaOriginal;
    private EstadosBarraSalud estadoBarraSalud;
    private NavMeshAgent agente;
    

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = MiniShooter.instance.AnimatorEnemigoTipo1;
        agente = GetComponent<NavMeshAgent>();

        saludActual = saludMaxima;
        escalaOriginal = barraDeSalud.transform.localScale;
        estadoBarraSalud = EstadosBarraSalud.verde;
        barraDeSalud.GetComponent<SpriteRenderer>().color = Color.green;

        GenerarRutaAleatoria();
        agente.SetDestination(puntosRuta[puntoRutaActual]);
    }

    void Update()
    {
        // regeneración de vida
        if (saludActual < saludMaxima)
        {
            saludActual += regeneracion * Time.deltaTime;
            if (saludActual > saludMaxima) saludActual = saludMaxima;
            ActualizarBarraSalud();
        }

        //transform.LookAt(MiniShooter.instance.PersonajePrincipal.position);


        if (Vector3.Distance(transform.position, MiniShooter.instance.PersonajePrincipal.position) > 10)
        {
            GetComponent<Animator>().SetBool("cerca", false);
            agente.SetDestination(puntosRuta[puntoRutaActual]);

            if (Vector3.Distance(transform.position, puntosRuta[puntoRutaActual]) < 1)
            {
                puntoRutaActual = (puntoRutaActual + 1) % puntosRuta.Length;
                agente.SetDestination(puntosRuta[puntoRutaActual]);
            }
        }        
        else if (Vector3.Distance(transform.position, MiniShooter.instance.PersonajePrincipal.position) < 2)
        {
            GetComponent<Animator>().SetBool("cerca", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("cerca", false);

            //persecución con navmesh
            agente.SetDestination(MiniShooter.instance.PersonajePrincipal.position);

            /*transform.position = Vector3.MoveTowards(transform.position,
                                                             MiniShooter.instance.PersonajePrincipal.transform.position,
                                                             MiniShooter.instance.VelocidadEnemigos * Time.deltaTime);*/
        }

       
    }

    // impacto con el enemigo, se agrega a la lista de enemigos eliminados
    public void Impacto(int puntos)
    {
        if (saludActual > puntos)
        {
            //restamos salud
            saludActual -= puntos;

            //actualizamos la barra de salud
            ActualizarBarraSalud();
        }
        else
        {
            MiniShooter.instance.AgregarEnemigoEliminado(this);
            enemigoImpactado.Invoke(++puntosEnemigo);
        }
    }

    void CambiarColorBarraSalud()
    {
        barraDeSalud.GetComponent<SpriteRenderer>().color = estadoBarraSalud switch
        {
            EstadosBarraSalud.verde => Color.green,
            EstadosBarraSalud.amarillo => Color.yellow,
            EstadosBarraSalud.rojo => Color.red,
            _ => Color.green
        };
    }

    void ActualizarBarraSalud()
    {
        float porcentajeVida = saludActual / saludMaxima;
        barraDeSalud.transform.localScale = new Vector3(escalaOriginal.x * porcentajeVida, barraDeSalud.transform.localScale.y, barraDeSalud.transform.localScale.z);

        // cambiamos el color de la barra según el porcentaje restante
        if (porcentajeVida > 0.8f && estadoBarraSalud != EstadosBarraSalud.verde)
        {
            estadoBarraSalud = EstadosBarraSalud.verde;
            CambiarColorBarraSalud();
        }
        else if (porcentajeVida > 0.2f && estadoBarraSalud != EstadosBarraSalud.amarillo)
        {
            estadoBarraSalud = EstadosBarraSalud.amarillo;
            CambiarColorBarraSalud();
        }
        else if (porcentajeVida <= 0.2f && estadoBarraSalud != EstadosBarraSalud.rojo)
        {
            estadoBarraSalud = EstadosBarraSalud.rojo;
            CambiarColorBarraSalud();
        }
    }

    // respwan del enemigo
    public void ReiniciarEnemigo()
    {
        Vector3 posicionRespawn;
        do
        {
            posicionRespawn = new Vector3(Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn),
                                          transform.position.y,
                                          Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn));

        } while (Vector3.Distance(MiniShooter.instance.PersonajePrincipal.position, posicionRespawn) < MiniShooter.instance.LadoZonaRespawn);

        transform.position = posicionRespawn;
        saludActual = saludMaxima;
        ActualizarBarraSalud();
    }


    private int numeroDePuntos = 10;
    private float distanciaMinima = 10f;
    private float distanciaMaxima = 20f;
    private Vector2 limiteX;
    private Vector2 limiteZ;

    

    void GenerarRutaAleatoria()
    {
        int intentosTotales = 0;
        puntosRuta = new Vector3[numeroDePuntos];

        MeshRenderer meshRenderer =  MiniShooter.instance.suelo.GetComponent<MeshRenderer>();
        Bounds bounds = meshRenderer.bounds;

        // Configura los límites basados en el suelo
        limiteX = new Vector2(bounds.min.x, bounds.max.x);
        limiteZ = new Vector2(bounds.min.z, bounds.max.z);

        Vector3 puntoAnterior = Vector3.zero;
        for (int i = 0; i < numeroDePuntos; i++)
        {
            Vector3 puntoAleatorio = GenerarPuntoAleatorio(puntoAnterior, distanciaMinima, distanciaMaxima);
            int intentos = 0;
            while (VerificarColision(puntoAleatorio) && intentos < 100)
            {
                puntoAleatorio = GenerarPuntoAleatorio(puntoAnterior, distanciaMinima, distanciaMaxima);
                intentos++;
                if(intentosTotales > 10000)
                {
                    Debug.LogError("No se pudo generar una ruta aleatoria");
                    break;
                }
            }
            puntoAnterior = puntoAleatorio;
            puntoAleatorio.y = 1;

            Debug.Log("Punto " + i + ": " + puntoAleatorio);

            puntosRuta[i] = puntoAleatorio;

            if (intentosTotales > 10000)
            {
                Debug.LogError("No se pudo generar una ruta aleatoria");
                break;
            }
        }
    }

    Vector3 GenerarPuntoAleatorio(Vector3 puntoAnterior, float minDist, float maxDist)
    {
        int intentosTotales = 10000;
        Vector3 direccionAleatoria;
        float distancia;
        Vector3 puntoAleatorio;
        do
        {
            direccionAleatoria = Random.insideUnitSphere * maxDist;
            direccionAleatoria += puntoAnterior;
            direccionAleatoria.y = 1; // Asegurar que el punto esté a nivel del suelo
            distancia = Random.Range(minDist, maxDist);
            puntoAleatorio = puntoAnterior + direccionAleatoria.normalized * distancia;

            if (intentosTotales > 10000)
            {
                Debug.LogError("No se pudo generar una ruta aleatoria");
                break;
            }
        } while (puntoAleatorio.x < limiteX.x || puntoAleatorio.x > limiteX.y || puntoAleatorio.z < limiteZ.x || puntoAleatorio.z > limiteZ.y);

        return puntoAleatorio;
    }

    bool VerificarColision(Vector3 punto)
    {
        return Physics.CheckSphere(punto, 0.5f); // Sin especificar LayerMask para detectar cualquier collider
    }
}
