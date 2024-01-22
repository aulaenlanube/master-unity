using UnityEngine;

// RequireComponent asegura que este script siempre tenga asociado un componente Rigidbody
[RequireComponent(typeof(CruzCubos))]
public class EjemploAtributosInspector : MonoBehaviour
{
    [Header("Configuraci�n de la cruz")]
    [Tooltip("Cantidad m�xima de cubos")]
    [SerializeField] private float velocidadMaximaRotacion = 1000f;

    [Header("Configuraci�n de Rotaci�n")]
    [Tooltip("Velocidad de rotaci�n de la cruz")]
    [Range(100, 1000)] // limita el rango de velocidad entre 100 y 1000
    public float velocidadRotacion = 100f;

    [Space(10)]
    [Tooltip("Altura de los cubos")]
    [Min(0)] // asegura que la altura del cubo no sea menor a 0
    public float alturaCubo = 1f;

    [Header("Informaci�n Adicional")]
    [TextArea(3, 10)] // crea un �rea de texto para escribir descripciones largas
    public string descripcion;

    // campo oculto en el inspector pero accesible desde otros scripts
    [HideInInspector] public float velocidadRotacionDefecto = 100f;

    private void Update()
    {        
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
    
    [ContextMenu("RotarHijos")] // agrega una opci�n al men� contextual en el inspector
    void RotarHijos()
    {
        GetComponent<CruzCubos>().RotarHijos();
    }

    [ContextMenu("RestaurarRotacion")] // agrega una opci�n al men� contextual en el inspector
    void RestaurarRotacion()
    {
        velocidadRotacion = velocidadRotacionDefecto;
        GetComponent<CruzCubos>().RestaurarRotacion();
    }

    [ContextMenuItem("Duplicar velocidad", "DuplicarVelocidad")]
    public float nuevaVelocidad;

    //m�todo para duplicar la velocidad (llamado desde ContextMenuItem)
    void DuplicarVelocidad()
    {
        nuevaVelocidad = velocidadRotacion * 2;
        if(nuevaVelocidad > velocidadMaximaRotacion) nuevaVelocidad = velocidadMaximaRotacion;       
        velocidadRotacion = nuevaVelocidad;
    }

}
