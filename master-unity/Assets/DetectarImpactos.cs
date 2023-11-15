using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarImpactos : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                // Comprobar si el objeto impactado tiene el script PuntuacionObjetivo
                PuntuacionDiana puntuacionObjetivo = hit.collider.GetComponent<PuntuacionDiana>();
                puntuacionObjetivo?.Impacto();

            }
            else Debug.Log("Nada");
        }
    }
}

