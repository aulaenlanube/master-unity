using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionCaminarEnemigoTipo1 : StateMachineBehaviour
{
    float reloj = 0;
    float intervalo = 0.1f;
    float distancia = 0.01f;
    Vector3 posicionInicial;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        posicionInicial = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        reloj += Time.deltaTime;
        if (reloj >= intervalo)
        {
            reloj = 0;

            //si el enemigo se ha movido muy poco en el último segundo, se considera bloqueado
            if(Vector3.Distance(animator.transform.position, posicionInicial) < distancia)
            {
                animator.SetBool("bloqueado", true);
            }
            posicionInicial = animator.transform.position;            
        }        
    }

}
