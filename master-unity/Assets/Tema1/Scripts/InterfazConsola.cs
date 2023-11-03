using UnityEngine;

public class InterfazConsola : MonoBehaviour
{  
    private void OnEnable()  { SeguidorRespawn.ActualizarPosicionRespawns += MostrarPosicionRespawn; }
    private void OnDisable() { SeguidorRespawn.ActualizarPosicionRespawns -= MostrarPosicionRespawn; }

    private void MostrarPosicionRespawn(Vector3 posicionRespawn)
    {
        Debug.Log($"Respawn en : {posicionRespawn}");        
    }
}
