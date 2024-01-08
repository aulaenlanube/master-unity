using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EjemploColeccionesList : MonoBehaviour
{
    public int cantidadDeCubos = 10;
    public float escalaMin = 1;
    public float escalaMax = 10;

    // Start is called before the first frame update
    void Start()
    {

        var inventario = new List<string>() { "Escudo", "Espada", "Arco", "Elixir", "Maza" };

        //métodos List
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

        //métodos de ordenación

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

        //ejercicio cubos
        GenerarYCargarCubos();
    }

    void mostrarInventario(List<string> inventario)
    {
        string s = "";
        foreach (string item in inventario) s += item + "-";
        Debug.Log(s);
    }

    void GenerarYCargarCubos()
    {
        var cubos = new List<GameObject>();        
        for (int i = 0; i < cantidadDeCubos; i++)
        {   
            float escalaCubo = Random.Range(escalaMin, escalaMax);
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.localScale = new Vector3(escalaCubo, escalaCubo, escalaCubo);
            cubo.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cubos.Add(cubo);
        }

        // ordenar los cubos por tamaño
        cubos.Sort((cubo1, cubo2) => cubo1.transform.localScale.magnitude.CompareTo(cubo2.transform.localScale.magnitude));

        // posicionar los cubos
        float zPosition = 0f;
        foreach (GameObject cubo in cubos)
        {
            cubo.transform.position = new Vector3(0, cubo.transform.localScale.y/2, zPosition);

            // ajustar la posición Z para el siguiente cubo
            zPosition += cubo.transform.localScale.z;
        }
    }

}
