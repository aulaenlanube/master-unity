using UnityEngine;

public class BarraSaludEnemigos : MonoBehaviour
{
    private GameObject barraSaludEnemigo;
    private EnemigoShooter enemigo;

    private void Start()
    {
        enemigo = transform.parent.gameObject.GetComponent<EnemigoShooter>();
        barraSaludEnemigo = enemigo.BarraSalud.transform.parent.gameObject;
    }

    void Update()
    {        
        if(enemigo.EstaHerido() && !enemigo.EstaMuerto())
        {
            barraSaludEnemigo.SetActive(true);
            transform.LookAt(MiniShooter.instance.PersonajePrincipal.position);
        }
        else
        {
            barraSaludEnemigo.SetActive(false);
        }        
    }
}
