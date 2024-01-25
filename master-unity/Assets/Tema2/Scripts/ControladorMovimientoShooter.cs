using UnityEngine;
using System;

public class ControladorMovimientoShooter : MonoBehaviour
{
    [SerializeField] private Vector3[] posicionesCamara;    
    private float rotacionVertical;    
    private int posicionActual;
    private float velocidadMovimiento;
    private float sensibilidadRaton;
    private float limiteRotacionVertical;

    void Start()
    {
        velocidadMovimiento = MiniShooter.instance.VelocidadPersonaje;
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        posicionActual = 0;
        rotacionVertical = 0;
        Cursor.visible = false; // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked; // bloqueamos el cursor en el centro de la pantalla    
    }

    void Update()
    {   
        // mover personaje
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * velocidadMovimiento * Time.deltaTime);      

        // rotación horizontal
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensibilidadRaton, 0);

        // rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);

        //disparo
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();            
        }

        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            Camera.main.transform.localPosition = posicionesCamara[++posicionActual % posicionesCamara.Length];
        }
    }

    void OnTriggerEnter(Collider collider)
    {        
        if (collider.CompareTag("Moneda"))
        {
            collider.gameObject.GetComponent<MonedaShooter>().RecolectarMoneda();
        }
        if (collider.CompareTag("Enemigo") || collider.CompareTag("Pared"))
        { 
            MiniShooter.instance.FinPartida();           
        }
    }
}
