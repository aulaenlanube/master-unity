using UnityEngine;

public class EnemigoShooter : MonoBehaviour
{    
    [SerializeField] private float velocidad = 5.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    private GameObject objetivo;

    private void Start()
    {
        objetivo = MiniShooter.instance.PersonajePrincipal();        
    }

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.transform.position);
        Vector3 pos = transform.position;

        // sigue al personaje
        if (distanciaObjetivo > 1)
        {
            transform.position = Vector3.MoveTowards(pos, objetivo.transform.position, velocidad * Time.deltaTime);
        }
        // si algún enemigo se acerca mucho, termina partida
        else
        {
            MiniShooter.instance.FinPartida();            
        }
    }

    public void DestruirObjetivo()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        CambiarPosicionObjetivo();
    }

    public void CambiarPosicionObjetivo()
    {
        transform.position = new Vector3(
            Random.Range(-ladoZonaRespawn, ladoZonaRespawn),
            transform.position.y,
            Random.Range(-ladoZonaRespawn, ladoZonaRespawn));
    }
}
