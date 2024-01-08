using System.Collections.Generic;
using UnityEngine;

public class EjemploOO : MonoBehaviour
{
    void Start()
    {
        Personaje guerrero = new Guerrero("Thorin", 100);
        Personaje mago = new Mago("Gandalf", 80);

        guerrero.RealizarAccionEspecial();  // ejecuta la versión de Guerrero
        mago.RealizarAccionEspecial();      // ejecuta la versión de Mago

        mago.RecibirGolpe(50);              // Gandalf ha recibido 50 puntos de daño
        guerrero.RecibirGolpe(50);          // Thorin ha recibido 45 puntos de daño

        mago.RecibirGolpe(50);              // Gandalf ha recibido 50 puntos de daño
        guerrero.RecibirGolpe(50);          // Thorin ha recibido 45 puntos de daño

        Debug.Log($"{guerrero.Nombre}:{guerrero.Vida} -- {mago.Nombre}:{mago.Vida}");

    }

    

}
