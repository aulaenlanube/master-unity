using UnityEngine;

public class AnimacionCamaraReposo : StateMachineBehaviour
{  
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        MiniShooter.instance.CamaraPrincipal.enabled = true;
        MiniShooter.instance.CamaraApuntando.enabled = false;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
