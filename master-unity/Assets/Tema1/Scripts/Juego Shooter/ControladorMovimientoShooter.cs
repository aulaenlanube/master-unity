using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float rotacionVertical = 0f;
    [SerializeField] private float limiteRotacionVertical = 45.0f; // l?mite de rotaci?n vertical

    void Start()
    {        
        Cursor.visible = false; // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked; // bloqueamos el cursor en el centro de la pantalla
    }

    void Update()
    {
        // movimiento adelante-atr?s
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;
        // movimiento izquierda-derecha
        float movimientoIzquierdaDerecha = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;

        // mover personaje
        transform.Translate(movimientoIzquierdaDerecha, 0, movimientoAdelanteAtras);


        // rotaci?n horizontal
        float rotacionRatonHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0, rotacionRatonHorizontal, 0);

        // rotaci?n vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);


        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) // si hay alg?n impacto
            {
                //establemos el impacto
                hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();
            }
        }
    }
}
