using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float escalaMaximaRebote = 0.2f; // Escala adicional máxima en Y para el rebote
    public float velocidadRebote = 3f;

    private Vector3 escalaOriginalCuerpo;
    private Vector3 posicionOriginalCuerpo;

    void Start()
    {
      
        escalaOriginalCuerpo = transform.localScale;
        posicionOriginalCuerpo = transform.localPosition;
    }

    void Update()
    {
        // Movimiento del cuerpo
        float movimientoH = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;
        float movimientoV = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;

        Vector3 movimiento = new Vector3(movimientoH, 0, movimientoV);
        transform.Translate(movimiento, Space.World);

        if (movimiento != Vector3.zero) // Si el cuerpo se está moviendo
        {
            float factorRebote = Mathf.Sin(Time.time * velocidadRebote) * escalaMaximaRebote;
            transform.localScale = new Vector3(
                escalaOriginalCuerpo.x,
                escalaOriginalCuerpo.y + factorRebote,
                escalaOriginalCuerpo.z
            );

            // Ajustar la posición del cuerpo para mantener la base en y = 0
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                posicionOriginalCuerpo.y + (transform.localScale.y - escalaOriginalCuerpo.y) / 2,
                transform.localPosition.z
            );

            
        }
    }
}


