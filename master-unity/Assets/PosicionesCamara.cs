using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionesCamara : MonoBehaviour
{
    [SerializeField] private Vector3[] posicionesCamara;
    private int posicionActual = 0;

    // Update is called once per frame
    void Update()
    {
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            transform.position = posicionesCamara[++posicionActual % posicionesCamara.Length];            
        }
    }        
}
