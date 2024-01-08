using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploColeccionesHashSet : MonoBehaviour
{    
    void Start()
    {
        Enemigo enemigo1 = new Enemigo("Orco", 100);
        Enemigo enemigo2 = new Enemigo("Elfo", 80);
        Enemigo enemigo3 = new Enemigo("Orco", 101);
        
        HashSet<Enemigo> enemigos = new HashSet<Enemigo>();
        enemigos.Add(enemigo1);
        enemigos.Add(enemigo2);
        enemigos.Add(enemigo3);

        foreach(Enemigo enemigo in enemigos)  Debug.Log(enemigo);        
    }

    private class Enemigo
    {
        private string nombre;
        private int vida;

        public Enemigo(string nombre, int vida)
        {
            this.nombre = nombre;
            this.vida = vida;
        }

        public override bool Equals(object obj)
        {
            return obj is Enemigo enemigo &&
                   nombre == enemigo.nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombre);
        }

        public override string ToString()
        {
            return $"Nombre: {nombre}, Puntos de Vida: {vida}";
        }
    }
}
