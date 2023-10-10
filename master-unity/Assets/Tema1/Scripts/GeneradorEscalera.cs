using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEscalera : MonoBehaviour
{
    public int escalones = 20;    
    public float anchoEscalon = 5f;
    public float profundidadEscalon = 2f;

    void Start()
    {
        for (int i = 0; i < escalones; i++)
        {
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.localScale = new Vector3(profundidadEscalon, 1, anchoEscalon);
            cubo.transform.position = new Vector3(i * profundidadEscalon, i, 0);
        }
    }
}
