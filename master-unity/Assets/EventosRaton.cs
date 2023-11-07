using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosRaton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Vector3 posicionRatonEnPantalla = Input.mousePosition;
            posicionRatonEnPantalla.z = 5f;
            Vector3 posicionRatonEnMundo = Camera.main.ScreenToWorldPoint(posicionRatonEnPantalla);

            Debug.Log("Posición del ratón en pantalla: " + posicionRatonEnPantalla);
            Debug.Log("Posición del ratón en el mundo: " + posicionRatonEnMundo);

            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayo, out hitInfo))
            {
                GameObject obj = hitInfo.collider.gameObject;
                if (obj != null)
                {
                    Debug.Log("Has hecho click en: " + obj.name);
                }
            }

        }
    }
}
