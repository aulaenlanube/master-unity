using UnityEngine;

public class AnimacionDisparoArma : StateMachineBehaviour
{  
    // al entrar al estado, reproduce el sonido de recarga
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniShooter.instance.EfectoDisparoTerceraPersona.gameObject.SetActive(true);
        MiniShooter.instance.ArmaTerceraPersona.GetComponent<Animator>().SetBool("disparando", true);
    }

    // al salir del estado, detiene el sonido de recarga
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniShooter.instance.EfectoDisparoTerceraPersona.gameObject.SetActive(false);
        MiniShooter.instance.ArmaTerceraPersona.GetComponent<Animator>().SetBool("disparando", false);
    }
}
