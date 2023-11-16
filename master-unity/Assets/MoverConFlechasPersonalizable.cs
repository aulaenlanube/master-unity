using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverConFlechasPersonalizable : MonoBehaviour
{
    public float velocidad = 5.0f;
    public KeyCode arriba;
    public KeyCode abajo;
    public KeyCode izquierda;
    public KeyCode derecha;

    void Update()
    {
        float despHorizontal = 0;
        float despVertical = 0;
        if (Input.GetKey(arriba)) despVertical = 1;
        else if (Input.GetKey(abajo)) despVertical = -1;
        else if (Input.GetKey(izquierda)) despHorizontal = -1;
        else if (Input.GetKey(derecha)) despHorizontal = 1;
        else if (Input.GetKey(KeyCode.Space)) gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

        // calcular el vector de desplazamiento
        Vector3 desplazamiento = new Vector3(despHorizontal, 0, despVertical);

        // mover el GameObject
        transform.Translate(desplazamiento * velocidad * Time.deltaTime);
    }
}
