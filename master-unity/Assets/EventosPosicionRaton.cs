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
            Debug.Log("Posici�n del rat�n en pantalla: " + posicionRatonEnPantalla);
        }
        if (Input.GetMouseButtonDown(1)) Debug.Log("Bot�n derecho del rat�n presionado");
        if (Input.GetMouseButton(1)) Debug.Log("Bot�n derecho del rat�n mantenido");
        if (Input.GetMouseButtonUp(1)) Debug.Log("Bot�n derecho del rat�n liberado");
    }
}
