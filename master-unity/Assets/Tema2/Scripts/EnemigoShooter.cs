using UnityEngine;
using UnityEngine.AI;

public enum EstadosBarraSalud
{
    verde,
    amarillo,
    rojo
}


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemigoShooter : MonoBehaviour
{
    [SerializeField] private GameObject barraDeSalud;
    [SerializeField] private float saludMaxima = 10f;
    [Range(10, 100)][SerializeField] private float distanciaSeguimiento = 10f;
    [Range(1, 3)][SerializeField] private float distanciaContacto = 1.5f;
    [Range(0, 10)][SerializeField] private float regeneracion = 1f;

    private int puntosEnemigo = 1;
    private int puntoRutaActual = 0;
    private float saludActual;
    private Vector3 escalaOriginal;
    private EstadosBarraSalud estadoBarraSalud;
    private NavMeshAgent agente;
    private Vector3[] puntosRuta;
    private float distanciaAlPersonaje;
    private Animator animator;
    private float intervaloActualizacionRuta = .5f;
    private float reloj;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    // LineRenderer para dibujar la ruta del enemigo
    private LineRenderer lineaAlObjetivo;

    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = MiniShooter.instance.AnimatorEnemigoTipo1;
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        saludActual = saludMaxima;
        escalaOriginal = barraDeSalud.transform.localScale;
        estadoBarraSalud = EstadosBarraSalud.verde;
        barraDeSalud.GetComponent<SpriteRenderer>().color = Color.green;
        reloj = intervaloActualizacionRuta;

        // obtenemos los puntos de la ruta
        puntosRuta = RutasEnemigos.instance.ObtenerRutaAleatoria();
        ObtenerPosicionAleatoria();

        lineaAlObjetivo = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // regeneración de vida
        RegenerarVida();

        // calculamos la distancia respecto al personaje principal
        distanciaAlPersonaje = Vector3.Distance(transform.position, MiniShooter.instance.PersonajePrincipal.position);



        // si el enemigo entra en contacto con el personaje principal, modificamos el booleano que activa la animación de ataque
        if (EnRangoAtaque())
        {
            animator.SetBool("cerca", true);
            return;
        }


        //si estamos suficientemente cerca del personaje, empezamos a seguirlo
        if (distanciaAlPersonaje < distanciaSeguimiento)
        {
            IrADestino(MiniShooter.instance.PersonajePrincipal.position);
        }
        // si no estamos cerca del personaje, seguimos la ruta
        else
        {
            // si estamos cerca de un punto de la ruta, obtenemos el siguiente punto de la ruta
            if (Vector3.Distance(transform.position, puntosRuta[puntoRutaActual]) < 1)
            {
                puntoRutaActual = (puntoRutaActual + 1) % puntosRuta.Length;
            }

            IrADestino(puntosRuta[puntoRutaActual]);
        }

        // dibujamos linea al objetivo
        DibujarRuta();

        // orientamos al agente hacia el siguiente punto de la ruta
        OrientarAgente();
    }


    // impacto con el enemigo, se agrega a la lista de enemigos eliminados
    public void Impacto(int puntos)
    {
        if (saludActual > puntos)
        {
            //activamos animación de golpe
            animator.SetBool("golpeado", true);

            //restamos salud
            saludActual -= puntos;

            //actualizamos la barra de salud
            ActualizarBarraSalud();
        }
        else
        {
            // ajustamos salud a 0 si está muerto
            saludActual = 0;

            //MiniShooter.instance.AgregarEnemigoEliminado(this);
            //enemigoImpactado.Invoke(++puntosEnemigo); // ----> hay que pasarlo en la máquina de estados, o en el mini-shooter

            animator.SetTrigger("muerto");
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

    void RegenerarVida()
    {
        if (EstaHerido() && !EstaMuerto()) // ----> si está herido y no está muerto, regeneramos vida
        {
            saludActual += regeneracion * Time.deltaTime;
            if (saludActual > saludMaxima) saludActual = saludMaxima;
            ActualizarBarraSalud();
        }
    }

    public bool EstaHerido()
    {
        return saludActual < saludMaxima;
    }

    public bool EstaMuerto()
    {
        return saludActual <= 0;
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
        ObtenerPosicionAleatoria();
        saludActual = saludMaxima;
        ActualizarBarraSalud();
    }

    void ObtenerPosicionAleatoria()
    {
        puntoRutaActual = Random.Range(0, puntosRuta.Length);
        EstablecerPosicion();
    }

    public bool EnRangoAtaque()
    {
        return distanciaAlPersonaje < distanciaContacto;
    }

    void EstablecerPosicion()
    {
        agente.enabled = false;
        transform.position = puntosRuta[puntoRutaActual];
        agente.enabled = true;
    }

    void DibujarRuta()
    {
        // dibujar la ruta del enemigo hasta el siguiente punto
        if (agente.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            lineaAlObjetivo.positionCount = agente.path.corners.Length;
            lineaAlObjetivo.SetPositions(agente.path.corners);
        }
        else
        {
            lineaAlObjetivo.positionCount = 0; // no dibujar si no hay ruta válida
        }
    }

    void OrientarAgente()
    {
        if (agente.path.corners.Length > 1)  // si hay más de un corner un 'corner' hacia el cual mirar
        {
            Vector3 posicionActual = transform.position;
            Vector3 proximoCorner = agente.path.corners[1];  // el índice 0 es la posición actual, el índice 1 es el próximo 'corner'

            Vector3 direccionHaciaCorner = proximoCorner - posicionActual;
            direccionHaciaCorner.y = 0;  // normalizar en el plano horizontal, ignorando la diferencia de altura

            // rotar hacia el próximo 'corner'
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaCorner);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 2);
        }
    }

    public void IrADestino(Vector3 destino)
    {
        reloj -= Time.deltaTime;
        if (reloj <= 0f)
        {
            NavMeshPath camino = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, destino, NavMesh.AllAreas, camino)
                && camino.status == NavMeshPathStatus.PathComplete)
            {
                animator.SetBool("bloqueado", false);
                agente.SetDestination(destino);
            }
            else
            {
                // intenta encontrar el punto más cercano válido en el NavMesh
                NavMeshHit hit;
                if (NavMesh.SamplePosition(destino, out hit, float.MaxValue, NavMesh.AllAreas))
                {
                    // distancia desde la posición actual del agente hasta el punto más cercano válido encontrado
                    float distanciaAlPuntoCercano = Vector3.Distance(transform.position, hit.position);

                    // si el punto es muy cercano respecto a la posición actual, se considera bloqueado
                    if (distanciaAlPuntoCercano < 2)
                    {
                        agente.ResetPath();
                    }
                    else
                    {
                        // si el punto es accesible, se establece como destino
                        if (NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, camino)  && camino.status == NavMeshPathStatus.PathComplete)
                        {
                            agente.SetDestination(hit.position);
                        }
                        else agente.SetDestination(puntosRuta[puntoRutaActual]);
                    }
                }
            }
            reloj = intervaloActualizacionRuta;  // reiniciamos el reloj
        }
    }

    public GameObject BarraSalud
    {
        get { return barraDeSalud; }
    }

    public int PuntosEnemigo
    {
        get { return puntosEnemigo; }
    }
}
