using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosPosicionRaton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posicionRatonEnPantalla = Input.mousePosition;  
            Debug.Log("Posición del ratón en pantalla: " + posicionRatonEnPantalla);
        }
        if (Input.GetMouseButtonDown(1)) Debug.Log("Botón derecho del ratón presionado");
        if (Input.GetMouseButton(1)) Debug.Log("Botón derecho del ratón mantenido");
        if (Input.GetMouseButtonUp(1)) Debug.Log("Botón derecho del ratón liberado");
    }
}
