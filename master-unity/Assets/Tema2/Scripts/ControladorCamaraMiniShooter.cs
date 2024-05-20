using UnityEngine;

public class ControladorCamaraMiniShooter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MiniShooter.instance.ReniciarCamara();
    }
}
