using UnityEngine;
using UnityEngine.AI;

public class AnimacionMuertoEnemigo1 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniShooter.instance.AgregarEnemigoEliminado(animator.gameObject.GetComponent<EnemigoShooter>());
    }
}
