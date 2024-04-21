using UnityEngine;
using UnityEngine.AI;

public class AnimacionParadoEnemigo1 : StateMachineBehaviour
{
    private float reloj = 0;
    private float intervalo = 0.1f; // cada 0.1 segundos se comprueba si el enemigo está bloqueado
    private float porcentajeMovimientoMinimo = 0.5f; // 10% de su movimiento esperado
    private float movMinimo;
    private Vector3 posicionInicial;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        posicionInicial = animator.transform.position;
        movMinimo = animator.gameObject.GetComponent<NavMeshAgent>().speed * intervalo * porcentajeMovimientoMinimo;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        reloj += Time.deltaTime;
        if (reloj >= intervalo)
        {
            reloj = 0;

            //si el enemigo se ha movido menos del 10% de su movimiento esperado en el último 0.1 segundo, se considera bloqueado
            if (Vector3.Distance(animator.transform.position, posicionInicial) > movMinimo)
            {
                animator.SetBool("bloqueado", false);
            }  
            posicionInicial = animator.transform.position;
        }
    }
}
