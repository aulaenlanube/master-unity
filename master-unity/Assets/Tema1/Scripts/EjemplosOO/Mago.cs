using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Personaje
    {
        public Mago(string nombre, int vida) : base(nombre, vida) { }
        public override void RealizarAccionEspecial()
        {
            Debug.Log($"{Nombre} lanza un hechizo mágico");
        }
    }


