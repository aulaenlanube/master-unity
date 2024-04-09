using UnityEngine;

public class AnimacionRecargaArma : StateMachineBehaviour
{  
    // al entrar al estado, reproduce el sonido de recarga
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniShooter.instance.ReproducirSonidoRecarga();
    }

    // al salir del estado, detiene el sonido de recarga
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniShooter.instance.DetenerEfectosDeSonido();
        MiniShooter.instance.CompletarRecarga();
    }
}
