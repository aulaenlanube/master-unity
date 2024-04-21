using UnityEngine;
using UnityEngine.AI;

public class AnimacionGolpeadoEnemigo1 : StateMachineBehaviour
{
    float velocidadOriginal;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // disminuir la velocidad del enemigo mientras está siendo golpeado
        velocidadOriginal = animator.gameObject.GetComponent<NavMeshAgent>().speed;
        animator.gameObject.GetComponent<NavMeshAgent>().speed = velocidadOriginal * 0.25f; //25% de la velocidad original
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("golpeado", false);

        // restablecer la velocidad del enemigo
        animator.gameObject.GetComponent<NavMeshAgent>().speed = velocidadOriginal;
    }
}
