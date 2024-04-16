using UnityEngine;

public class BarraSaludEnemigos : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(MiniShooter.instance.PersonajePrincipal.position);
    }
}
