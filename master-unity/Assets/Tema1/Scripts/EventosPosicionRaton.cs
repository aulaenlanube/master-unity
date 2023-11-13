using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosPosicionRaton : MonoBehaviour
{
    public float z = 15f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posicionRatonEnPantalla = Input.mousePosition;  
            Debug.Log("Posición del ratón en pantalla: " + posicionRatonEnPantalla);

            posicionRatonEnPantalla.z = z;
            Vector3 posicionRatonEnMundo = Camera.main.ScreenToWorldPoint(posicionRatonEnPantalla);
            Debug.Log($"El punto en el espacio del mundo es: {posicionRatonEnMundo}");
        }
        if (Input.GetMouseButtonDown(1)) Debug.Log("Botón derecho del ratón presionado");
        if (Input.GetMouseButton(1)) Debug.Log("Botón derecho del ratón mantenido");
        if (Input.GetMouseButtonUp(1)) Debug.Log("Botón derecho del ratón liberado");
    }
}
