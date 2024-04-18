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

    private int puntosEnemigo = 0;
    private int puntoRutaActual = 0;
    private float saludActual;
    private Vector3 escalaOriginal;
    private EstadosBarraSalud estadoBarraSalud;
    private NavMeshAgent agente;
    private Vector3[] puntosRuta;
    private float distanciaAlPersonaje;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    // LineRenderer para dibujar la ruta del enemigo
    private LineRenderer lineaAlObjetivo;

    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = MiniShooter.instance.AnimatorEnemigoTipo1;
        agente = GetComponent<NavMeshAgent>();

        saludActual = saludMaxima;
        escalaOriginal = barraDeSalud.transform.localScale;
        estadoBarraSalud = EstadosBarraSalud.verde;
        barraDeSalud.GetComponent<SpriteRenderer>().color = Color.green;

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
            GetComponent<Animator>().SetBool("cerca", true);
            return;
        }

        //si estamos suficientemente cerca del personaje, empezamos a seguirlo
        if (distanciaAlPersonaje < distanciaSeguimiento)
        {  
            agente.SetDestination(MiniShooter.instance.PersonajePrincipal.position);  
        }
        // si no estamos cerca del personaje, seguimos la ruta
        else
        {
            // si estamos cerca de un punto de la ruta, obtenemos el siguiente punto de la ruta
            if (Vector3.Distance(transform.position, puntosRuta[puntoRutaActual]) < 1)
            {
                puntoRutaActual = (puntoRutaActual + 1) % puntosRuta.Length;
            }

            agente.SetDestination(puntosRuta[puntoRutaActual]);   
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

    void RegenerarVida()
    {
        if (saludActual < saludMaxima)
        {
            saludActual += regeneracion * Time.deltaTime;
            if (saludActual > saludMaxima) saludActual = saludMaxima;
            ActualizarBarraSalud();
        }
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
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime*2);            
        }
    }
}
