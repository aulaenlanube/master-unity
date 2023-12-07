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


        /*List<string> inventario = new List<string>() { "Escudo", "Espada", "Arco", "Elixir", "Maza" };

        inventario.Add("Escudo");
        mostrarInventario(inventario);
        inventario.Remove("Escudo");
        mostrarInventario(inventario);
        if (inventario.Count > 1) inventario.RemoveAt(1); // elimina el segundo elemento
        mostrarInventario(inventario);
        bool tieneEspada = inventario.Contains("Espada");
        Debug.Log("bool: " + tieneEspada);
        int cantidadItems = inventario.Count;
        Debug.Log("count: " + cantidadItems);
        int indiceEspada = inventario.IndexOf("espada");
        Debug.Log("El indiceEspada es " + indiceEspada);

        inventario.Insert(0, "Poción"); // añade "Poción" al principio de la lista
        mostrarInventario(inventario);

        inventario.Clear();
        mostrarInventario(inventario);

        List<int> numeros = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5 };
        numeros.Sort((a, b) => b.CompareTo(a)); // 9, 6, 5, 5, 4, 2, 3, 1, 1
        numeros.ForEach(n => Debug.Log(n));

        List<string> palabras = new List<string> { "aab", "aa", "abc", "b", "aaa", "a" };
        palabras.Sort() ; // a aa aaa aab abc b
        palabras.ForEach(s => Debug.Log(s));

        List<string> palabras2 = new List<string> { "aab", "aa", "abc", "b", "aaa", "a" };
        palabras2.Sort((a, b) => a.Length.CompareTo(b.Length)); //  b a aa aab abc aaa
        palabras2.ForEach(s => Debug.Log(s));
        palabras2.OrderBy(s => s.Length).ToList().ForEach(s => Debug.Log(s)); // con System.Linq
        */


        HashSet<int> listaNumeros = new HashSet<int> { 1, 5, 8, 8, 1 };
        listaNumeros.Add(4);
        listaNumeros.Add(5);
        listaNumeros.Add(6);
        listaNumeros.Remove(1);

        foreach (int num in listaNumeros) Debug.Log(num); // 6-8-4-6

        HashSet<string> jugadores = new HashSet<string>();
        jugadores.Add("Jugador1");
        jugadores.Add("Jugador2");
        jugadores.Add("Jugador2");
        jugadores.Add("Jugador3");
        foreach (string jugador in jugadores) Debug.Log(jugador);
    }

    void mostrarInventario(List<string> inventario)
    {
        string s = "";
        foreach (string item in inventario) s += item + "-";
        Debug.Log(s);
    }

}
