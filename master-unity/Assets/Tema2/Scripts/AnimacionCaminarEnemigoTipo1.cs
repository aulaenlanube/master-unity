using UnityEngine;
using UnityEngine.AI;

public class AnimacionCaminarEnemigoTipo1 : StateMachineBehaviour
{
    private float reloj = 0;
    private float intervalo = 0.1f; 
    private float porcentajeMovimientoMinimo = 0.1f; // % de su movimiento esperado
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
            if (Vector3.Distance(animator.transform.position, posicionInicial) < movMinimo)
            {                
                animator.SetBool("bloqueado", true);
            }  
            posicionInicial = animator.transform.position;            
        }        
    }
}
