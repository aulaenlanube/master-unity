using UnityEngine;
public enum EstadosBarraSalud
{
    verde,
    amarillo,
    rojo
}

[RequireComponent(typeof(Animator))]
public class EnemigoShooter : MonoBehaviour
{
    [SerializeField] private GameObject barraDeSalud;
    [SerializeField] private float saludMaxima = 10f;
    [Range(0, 10)][SerializeField] private float regeneracion = 1f;

    private int puntosEnemigo = 0;
    private float saludActual;
    private Vector3 escalaOriginal;
    private EstadosBarraSalud estadoBarraSalud;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = MiniShooter.instance.AnimatorEnemigoTipo1;

        saludActual = saludMaxima;
        escalaOriginal = barraDeSalud.transform.localScale;
        estadoBarraSalud = EstadosBarraSalud.verde;
        barraDeSalud.GetComponent<SpriteRenderer>().color = Color.green;
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

        transform.LookAt(MiniShooter.instance.PersonajePrincipal.position);

        if (Vector3.Distance(transform.position, MiniShooter.instance.PersonajePrincipal.position) < 2)
        {
            GetComponent<Animator>().SetBool("cerca", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("cerca", false);

            transform.position = Vector3.MoveTowards(transform.position,
                                                             MiniShooter.instance.PersonajePrincipal.transform.position,
                                                             MiniShooter.instance.VelocidadEnemigos * Time.deltaTime);
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
}
