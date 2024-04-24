using UnityEngine;
using UnityEngine.AI;

public class AnimacionAtaqueEnemigoTipo1 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // detenemos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        // iniciamos el sonido de recibir daño
        MiniShooter.instance.ReproducirSonidoDolorPersonaje();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // mientras ataca el enemigo, este mira al personaje principal y le quita vida teniendo en cuenta el dps
        animator.gameObject.transform.LookAt(MiniShooter.instance.PersonajePrincipal);
        MiniShooter.instance.PersonajePrincipal.GetComponent<SaludJugadorShooter>().RecibirGlope(animator.gameObject.GetComponent<EnemigoShooter>().Dps);

        // si el personaje se aleja, modificamos el booleano que desactiva la animación de ataque
        if (!animator.gameObject.GetComponent<EnemigoShooter>().EnRangoAtaque())
        {
            animator.SetBool("cerca", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //detenemos el sonido de recibir daño
        MiniShooter.instance.DetenerEfectosDeSonido();

        // reanudamos el movimiento del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = false;

        // volvemos a seguir al personaje principal
        animator.gameObject.GetComponent<EnemigoShooter>().IrADestino(MiniShooter.instance.PersonajePrincipal.position);
    }
}
