using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCubo : MonoBehaviour
{
    public float velocidad = 5f;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float movimiento = velocidad * Time.deltaTime;

        // mueve el cubo de izquierda a derecha y viceversa al llegar a un límite
        transform.position += new Vector3(movimiento, 0, 0);
        if (transform.position.x > 5 || transform.position.x < -5)
        {
            velocidad = -velocidad; // invertimos la dirección
                                    //cambiamos el color del material
            rend.material.color = Random.ColorHSV();
        }

        //cambiamos color en base a la posición en el eje Y
        rend.material.color = transform.position.y switch
        {
            > 5 => Color.green,
            < -5 => Color.red,
            _ => Color.blue
        };
    }
}
