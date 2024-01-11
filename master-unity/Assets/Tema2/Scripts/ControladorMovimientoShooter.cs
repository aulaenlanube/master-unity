using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    public float velocidadMovimiento = 10f;
    public float sensibilidadRaton = 10f;
    private float rotacionVertical = 0f;
    public float limiteRotacionVertical = 45.0f; // límite de rotación vertical
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // movimiento adelante-atrás
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;
        // movimiento izquierda-derecha
        float movimientoIzquierdaDerecha = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;        

        // mover personaje
        transform.Translate(movimientoIzquierdaDerecha, 0, movimientoAdelanteAtras);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;



        // rotación horizontal
        float rotacionRatonHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0, rotacionRatonHorizontal, 0);

        // rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);        
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);

        

        
    }
}
