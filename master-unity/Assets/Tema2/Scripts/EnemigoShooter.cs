using UnityEngine;

public class EnemigoShooter : MonoBehaviour
{  
    private int puntosEnemigo = 0;
    private float fuerzaDeEmpuje = 10f;
    private float fuerzaDeTorque = 10f;

    // evento para actualizar la puntuación
    public delegate void impacto(int puntos);
    public static event impacto enemigoImpactado;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                 MiniShooter.instance.PersonajePrincipal.transform.position,
                                                 MiniShooter.instance.VelocidadEnemigos * Time.deltaTime);
    }

    // impacto con el enemigo, se agrega a la lista de enemigos eliminados
    public void DestruirObjetivo()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(); 
        MiniShooter.instance.AgregarEnemigoEliminado(this);        
        enemigoImpactado.Invoke(++puntosEnemigo);
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
                Vector3 direccionDeEmpuje = collision.transform.position - transform.position;
                direccionDeEmpuje = direccionDeEmpuje.normalized * fuerzaDeEmpuje;
                rb.AddForce(direccionDeEmpuje, ForceMode.Impulse);

                // aplicar una fuerza de torque
                Vector3 torque = Random.insideUnitSphere * fuerzaDeTorque;
                rb.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }

}
