using UnityEngine;
using UnityEngine.AI;

public class AnimacionGolpeadoEnemigo1 : StateMachineBehaviour
{
    float velocidadOriginal;
    float factorRalentizacion = 0.4f; // 40% de la velocidad original

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       velocidadOriginal = animator.gameObject.GetComponent<NavMeshAgent>().speed;
        animator.gameObject.GetComponent<NavMeshAgent>().speed = velocidadOriginal * factorRalentizacion; 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        animator.SetBool("golpeado", false);
        animator.gameObject.GetComponent<NavMeshAgent>().speed = velocidadOriginal;
    }
}
