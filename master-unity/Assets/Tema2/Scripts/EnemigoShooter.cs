using Unity.VisualScripting;
using UnityEngine;

public class EnemigoShooter : MonoBehaviour
{
    [SerializeField] private float velocidad = 5.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    private Transform objetivo;
    private int puntosEnemigo = 1;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;


    private void Start()
    {
        objetivo = MiniShooter.instance.PersonajePrincipal();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, velocidad * Time.deltaTime);       
    }

    // impacto con el enemigo: cambia su posición, incrementa su velocidad y actualiza la puntuación
    public void DestruirObjetivo()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        CambiarPosicionObjetivo();
        IncrementarVelocidad();
        puntosEnemigo++;
        enemigoImpactado?.Invoke(puntosEnemigo);
    }

    // respwan del enemigo
    public void CambiarPosicionObjetivo()
    {
        // distancia mínima de respawn como el 50% del lado de la zona de respawn
        float distanciaMinimaRespawn = ladoZonaRespawn / 2;

        Vector3 posicionRespawn;
        do
        {
            posicionRespawn = new Vector3(Random.Range(-ladoZonaRespawn, ladoZonaRespawn), transform.position.y, Random.Range(-ladoZonaRespawn, ladoZonaRespawn));
        } while (Vector3.Distance(objetivo.position, posicionRespawn) < ladoZonaRespawn / 2);

        transform.position = new Vector3(posicionRespawn.x, transform.position.y, posicionRespawn.y);
    }

    // incrementa la velocidad del enemigo en un 10%
    public void IncrementarVelocidad()
    {
        velocidad *= 1.1f;
    }
}
