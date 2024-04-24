using UnityEngine;
using UnityEngine.AI;

public class AnimacionAtaqueEnemigoTipo1 : StateMachineBehaviour
{
    private float dps;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // obtenemos el dps del enemigo
        dps = animator.gameObject.GetComponent<EnemigoShooter>().Dps;

        // detenemos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // mientras ataca el enemigo, este mira al personaje principal y le quita vida teniendo en cuenta el dps
        animator.gameObject.transform.LookAt(MiniShooter.instance.PersonajePrincipal);
        SaludJugadorShooter.instance.RecibirGlope(dps);        
      
        // si el personaje se aleja, modificamos el booleano que desactiva la animación de ataque
        if (!animator.gameObject.GetComponent<EnemigoShooter>().EnRangoAtaque())
        {
            animator.SetBool("cerca", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reanudamos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = false;

        // volvemos a seguir al personaje principal
        animator.gameObject.GetComponent<EnemigoShooter>().IrADestino(MiniShooter.instance.PersonajePrincipal.position);
    }
}
