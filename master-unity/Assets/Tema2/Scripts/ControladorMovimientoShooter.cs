using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    public float velocidadMovimiento = 10f;
    public float sensibilidadRaton = 10f;
    private float rotacionVertical = 0f;
    public float limiteRotacionVertical = 45.0f; // límite de rotación vertical

    void Update()
    {
        // movimiento adelante-atrás
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento;
        movimientoAdelanteAtras *= Time.deltaTime;

        // movimiento izquierda-derecha
        float movimientoIzquierdaDerecha = Input.GetAxis("Horizontal") * velocidadMovimiento;        
        movimientoIzquierdaDerecha *= Time.deltaTime;

        transform.Translate(movimientoIzquierdaDerecha, 0, movimientoAdelanteAtras);

        // rotación horizontal
        float rotacionRatonHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0, rotacionRatonHorizontal, 0);

        // rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);        
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);
    }

}
