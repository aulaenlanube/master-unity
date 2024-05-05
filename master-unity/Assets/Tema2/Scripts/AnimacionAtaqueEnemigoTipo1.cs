using UnityEngine;
using UnityEngine.AI;

public class AnimacionAtaqueEnemigoTipo1 : StateMachineBehaviour
{
    private float dps;
    private EnemigoShooter enemigoActual;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemigoActual = animator.gameObject.GetComponent<EnemigoShooter>();

        // obtenemos el dps del enemigo
        dps = animator.gameObject.GetComponent<EnemigoShooter>().Dps;

        // detenemos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // mientras ataca el enemigo, este mira al personaje principal y le quita vida teniendo en cuenta el dps
        animator.gameObject.transform.LookAt(MiniShooter.instance.PersonajeTerceraPersona);
        SaludJugadorShooter.instance.RecibirGlope(dps);        
      
        // si el personaje se aleja, modificamos el booleano que desactiva la animación de ataque
        if (!enemigoActual.EnRangoAtaque() || enemigoActual.EstaMuerto())
        {
            animator.SetBool("cerca", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!enemigoActual.EstaMuerto())
        {
            // reanudamos el movimiento del enemigo
            animator.gameObject.GetComponent<NavMeshAgent>().isStopped = false;

            // volvemos a seguir al personaje principal
            animator.gameObject.GetComponent<EnemigoShooter>().IrADestino(MiniShooter.instance.PersonajeTerceraPersona.position);
        }        
    }
}
