using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
public class EnemigoShooter : MonoBehaviour
{
    [SerializeField] private GameObject barraDeVida;
    [SerializeField] private float saludMaxima = 10f;  

    private int puntosEnemigo = 0;
    private float fuerzaDeEmpuje = 10f;
    private float saludActual;
    private Vector3 escalaOriginal;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    void Start()
    {
        
        GetComponent<Animator>().runtimeAnimatorController = MiniShooter.instance.AnimatorEnemigoTipo1;

        saludActual = saludMaxima;
        escalaOriginal = barraDeVida.transform.localScale;
        ActualizarBarraSalud();
    }

    void Update()
    {
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
    public void DestruirObjetivo(int puntos)
    {
        if (saludActual > puntos)
        {
            //restamos salud
            saludActual -= puntos;

            //actualizamos la barra de vida
            ActualizarBarraSalud();
        }
        else
        {
            MiniShooter.instance.AgregarEnemigoEliminado(this);
            enemigoImpactado.Invoke(++puntosEnemigo);

            //aplicar animación de muerte
        }
    }

    void ActualizarBarraSalud()
    {
        float porcentajeVida = saludActual / saludMaxima;
        barraDeVida.transform.localScale = new Vector3(escalaOriginal.x * porcentajeVida, barraDeVida.transform.localScale.y, barraDeVida.transform.localScale.z);

        

        // cambiamos el color de la barra según el porcentaje restante
        if (porcentajeVida > 0.8f) barraDeVida.GetComponent<SpriteRenderer>().color = Color.green;
        else if (porcentajeVida > 0.2f) barraDeVida.GetComponent<SpriteRenderer>().color = Color.yellow;
        else barraDeVida.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // respwan del enemigo
    public void CambiarPosicion()
    {
        Vector3 posicionRespawn;
        do
        {
            posicionRespawn = new Vector3(Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn),
                                          transform.position.y,
                                          Random.Range(-MiniShooter.instance.LadoZonaRespawn, MiniShooter.instance.LadoZonaRespawn));

        } while (Vector3.Distance(MiniShooter.instance.PersonajePrincipal.position, posicionRespawn) < MiniShooter.instance.LadoZonaRespawn);

        transform.position = posicionRespawn;
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemigoShooter enemigo = collision.gameObject.GetComponent<EnemigoShooter>();
        if (enemigo != null)
        {
            // aplicar una fuerza para separar ambos objetos
            Rigidbody rb = collision.rigidbody;
            if (rb != null)
            {
                // calculamos la dirección y la fuerza del empuje
                Vector3 direccionDeEmpuje = collision.transform.position - transform.position;
                direccionDeEmpuje = direccionDeEmpuje.normalized * fuerzaDeEmpuje;
                rb.AddForce(direccionDeEmpuje, ForceMode.Impulse);
            }
        }
    }

}
