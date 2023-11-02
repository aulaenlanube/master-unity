using UnityEngine;
using UnityEngine.UI;

public class InterfazUsuario : MonoBehaviour
{
    [SerializeField] private Text cantidadRespawns;

    private void OnEnable()     { SeguidorRespawn.ActualizarCantidadRespawns += ActualizarCantidadRespawns; }
    private void OnDisable()    { SeguidorRespawn.ActualizarCantidadRespawns -= ActualizarCantidadRespawns; }

    private void ActualizarCantidadRespawns(int cantidadRespawns)
    {
        this.cantidadRespawns.text = $"Respawns: {cantidadRespawns}";
    }
}
