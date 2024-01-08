using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploColeccionesDiccionario : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, int> inventario = new Dictionary<string, int>();

        inventario.Add("poción", 5);
        inventario.Add("elixir", 3);

        if (inventario.TryGetValue("poción", out int cantidadActual)) //devuelve booleano
            inventario["poción"] = cantidadActual + 2;

        if (inventario.ContainsKey("poción"))
            Debug.Log($"El jugador tiene alguna poción en el inventario");

        if (inventario.TryGetValue("poción", out int valor))
            Debug.Log($"El jugador tiene {valor} pociones en el inventario");

        if (inventario.ContainsValue(3))
            Debug.Log($"El jugador tiene cantidad {valor} de algún elemento del inventario");

        Debug.Log($"El jugador tiene {inventario.Count} objetos distintos en el inventario");

        foreach (KeyValuePair<string, int> item in inventario)
            Debug.Log($"Item: {item.Key}, Cantidad: {item.Value}");

    }

}
