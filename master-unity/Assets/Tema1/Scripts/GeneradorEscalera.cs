using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEscalera : MonoBehaviour
{
    public int escalones = 20;    
    public float anchoEscalon = 5f;
    public float profundidadEscalon = 1f;
    public float alturaEscalon = 2f;

    void Start()
    {
        for (int i = 0; i < escalones; i++)
        {
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.localScale = new Vector3(profundidadEscalon, alturaEscalon, anchoEscalon);
            cubo.transform.position = new Vector3(i * profundidadEscalon, i * alturaEscalon, 0);
        }
    }
}
