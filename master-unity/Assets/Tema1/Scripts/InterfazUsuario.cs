using UnityEngine;
using UnityEngine.UI;

public class InterfazUsuario : MonoBehaviour
{
    [SerializeField] private Text cantidadRespawns;

    private void OnEnable()     { SeguidorRespawn.respawn += ActualizarRespawns; }
    private void OnDisable()    { SeguidorRespawn.respawn -= ActualizarRespawns; }

    private void ActualizarRespawns(int cantidadRespawns, Vector3 posicionRespawn)
    {
        this.cantidadRespawns.text = $"Respawns: {cantidadRespawns}";
        Debug.Log($"Respawn en : {posicionRespawn}");
    }
}
