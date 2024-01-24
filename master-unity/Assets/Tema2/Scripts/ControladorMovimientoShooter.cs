using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float rotacionVertical = 0f;
    [SerializeField] private float limiteRotacionVertical = 45.0f; // límite de rotación vertical
    [SerializeField] private Vector3[] posicionesCamara;
    private int posicionActual = 0;

    void Start()
    {        
        Cursor.visible = false; // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked; // bloqueamos el cursor en el centro de la pantalla
    }

    void Update()
    {
        // movimiento adelante-atrás
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;
        // movimiento izquierda-derecha
        float movimientoIzquierdaDerecha = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;

        // mover personaje
        transform.Translate(movimientoIzquierdaDerecha, 0, movimientoAdelanteAtras);


        // rotación horizontal
        float rotacionRatonHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0, rotacionRatonHorizontal, 0);

        // rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);

        //disparo
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) // si hay algún impacto
            {
                //establemos el impacto
                hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();
            }
        }

        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            Camera.main.transform.localPosition = posicionesCamara[++posicionActual % posicionesCamara.Length];
        }
    }

    void OnTriggerEnter(Collider colider)
    {        
        if (colider.gameObject.tag == "Pared" || colider.gameObject.tag == "Enemigo")
        {
            MiniShooter.instance.FinPartida();
        }            
    }
}
