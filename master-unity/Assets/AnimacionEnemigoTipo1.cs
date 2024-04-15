using UnityEngine;
using UnityEngine.AI;

public class AnimacionEnemigoTipo1 : StateMachineBehaviour
{    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // detenemos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        // mientras ataca el enemigo, este mira al personaje principal
        animator.gameObject.transform.LookAt(MiniShooter.instance.PersonajePrincipal);

        // si el personaje se aleja, modificamos el booleano que desactiva la animación de ataque
        if(!animator.gameObject.GetComponent<EnemigoShooter>().EnRangoAtaque())
        {
            animator.SetBool("cerca", false);
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reanudamos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = false;

        // volvemos a seguir al personaje principal
        animator.gameObject.GetComponent<NavMeshAgent>().SetDestination(MiniShooter.instance.PersonajePrincipal.position);
    }
}
