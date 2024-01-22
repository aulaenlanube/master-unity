using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class CruzCubos : MonoBehaviour
{    
    [SerializeField] private int rotacion = 30;
    private List<Transform> hijos;

    private void Start()
    {
        hijos = new List<Transform>();
        foreach (Transform hijo in transform) hijos.Add(hijo);
    }

    public void RotarHijos()
    {
        foreach (Transform hijo in hijos) hijo.Rotate(Vector3.up * rotacion);               
    }
    
    public void RestaurarRotacion()
    {
        foreach (Transform hijo in hijos) hijo.localRotation = Quaternion.identity;        
    }
}
