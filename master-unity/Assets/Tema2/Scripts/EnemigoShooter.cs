using UnityEngine;

public class EnemigoShooter : MonoBehaviour
{
    private float velocidad;
    private int ladoZonaRespawn;
    private Transform objetivo;
    private int puntosEnemigo = 1;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    private void Start()
    {
        objetivo = MiniShooter.instance.PersonajePrincipal;
        velocidad = MiniShooter.instance.VelocidadEnemigos;
        ladoZonaRespawn = MiniShooter.instance.LadoZonaRespawn;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 objetivo.transform.position, 
                                                 velocidad * Time.deltaTime);
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
        Vector3 posicionRespawn;
        do
        {
            posicionRespawn = new Vector3(Random.Range(-ladoZonaRespawn, ladoZonaRespawn),
                                          transform.position.y,
                                          Random.Range(-ladoZonaRespawn, ladoZonaRespawn));

        } while (Vector3.Distance(objetivo.position, posicionRespawn) < ladoZonaRespawn);

        transform.position = new Vector3(posicionRespawn.x, transform.position.y, posicionRespawn.y);
    }

    // incrementa la velocidad del enemigo en un 10%
    public void IncrementarVelocidad()
    {
        velocidad *= 1.1f;
    }
}
