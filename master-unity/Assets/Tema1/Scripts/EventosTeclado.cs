using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosTeclado : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("Tecla Espacio presionada");
        if (Input.GetKey(KeyCode.A)) Debug.Log("Tecla A mantenida");
        if (Input.GetKeyUp(KeyCode.S)) Debug.Log("Tecla S liberada");

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Has pulsado A y D al mismo tiempo");
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Has pulsado A, S y D al mismo tiempo");
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Guardando el juego...");
        }

    }


}
