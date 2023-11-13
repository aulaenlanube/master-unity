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
            Debug.Log("Posici�n del rat�n en pantalla: " + posicionRatonEnPantalla);

            posicionRatonEnPantalla.z = z;
            Vector3 posicionRatonEnMundo = Camera.main.ScreenToWorldPoint(posicionRatonEnPantalla);
            Debug.Log($"El punto en el espacio del mundo es: {posicionRatonEnMundo}");
        }
        if (Input.GetMouseButtonDown(1)) Debug.Log("Bot�n derecho del rat�n presionado");
        if (Input.GetMouseButton(1)) Debug.Log("Bot�n derecho del rat�n mantenido");
        if (Input.GetMouseButtonUp(1)) Debug.Log("Bot�n derecho del rat�n liberado");
    }
}
