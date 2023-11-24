using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemplosRecursividad : MonoBehaviour
{
    private void Start()
    {
        Saludar(5);
        DesactivarHijosRecursivamente(transform);
        if(BuscarObjetoRecursivamente(transform, "aulaenlanube") != null) Debug.Log("aulaenlanube.com encontrado");
        GenerarEscalera(Vector3.zero, 1, 20);        
    }

    void Saludar(int n)
    {   
        //caso base
        if (n == 0)         
        {
            Debug.Log($"Bye");
        }
        // caso recursivo 
        else 
        {
            Debug.Log($"n:{n}");
            Saludar(--n);  
        }
    }

    void GenerarEscalera(Vector3 posInicial, int alturaEscalon, int escalones)
    {
        if (alturaEscalon > escalones) return;

        // instanciar un nuevo cubo en la posición inicial con la altura especificada
        GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubo.transform.localScale = new Vector3(1, alturaEscalon, 1);
        cubo.transform.position = posInicial + new Vector3(0, alturaEscalon / 2f, 0);

        // avanzamos 1 unidad en el eje X 
        posInicial += Vector3.right;

        // llamada recursiva para crear el siguiente escalón
        GenerarEscalera(posInicial, alturaEscalon + 1, escalones);
    }
    void DesactivarHijosRecursivamente(Transform padre)
    {
        foreach (Transform hijo in padre)
        {
            hijo.gameObject.SetActive(false);
            DesactivarHijosRecursivamente(hijo);
        }
    }

    GameObject BuscarObjetoRecursivamente(Transform padre, string nombreBuscado)
    {
        if (padre.name == nombreBuscado) return padre.gameObject;
        foreach (Transform hijo in padre)
        {
            GameObject encontrado = BuscarObjetoRecursivamente(hijo, nombreBuscado);
            if (encontrado != null) return encontrado;
        }
        return null;
    }




}
