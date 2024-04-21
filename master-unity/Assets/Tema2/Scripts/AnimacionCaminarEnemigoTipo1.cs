using UnityEngine;
using UnityEngine.AI;

public class AnimacionCaminarEnemigoTipo1 : StateMachineBehaviour
{
    private float reloj = 0;
    private float intervalo = 0.1f; // cada 0.1 segundos se comprueba si el enemigo est� desbloqueado
    private float porcentajeMovimientoMinimo = 0.5f; // 50% de su movimiento esperado
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

            //si el enemigo se ha movido menos del 10% de su movimiento esperado en el �ltimo 0.1 segundo, se considera bloqueado
            if (Vector3.Distance(animator.transform.position, posicionInicial) < movMinimo)
            {                
                animator.SetBool("bloqueado", true);
            }  
            posicionInicial = animator.transform.position;            
        }        
    }
}
