using TMPro;
using UnityEngine;

public class DibujarLineaEntreCubos : MonoBehaviour
{   
    [Range(0.05f,1f)]
    public float anchoLinea;
    public Color colorInicio;
    public Color colorFin;
    public Material materialLinea;
    public Transform cubo1;
    public Transform cubo2;
    public TextMeshPro textoDistancia;
    public TextMeshPro textoCubo1;
    public TextMeshPro textoCubo2;

    private LineRenderer linea;

    void Start()
    {        
        linea = gameObject.AddComponent<LineRenderer>();
        ActualizarCaracteristicasLinea();
    }

    void Update()
    {
        ActualizarCaracteristicasLinea();
        
        //texto línea
        textoDistancia.text = Vector3.Distance(cubo1.position, cubo2.position).ToString("F2"); 
        textoDistancia.transform.position = (cubo1.position + cubo2.position) / 2 + Vector3.up; 

        //texto cubo1
        textoCubo1.text = cubo1.position.ToString("F2");
        textoCubo1.transform.position = cubo1.position + Vector3.up; 

        //texto cubo2
        textoCubo2.text = cubo2.position.ToString("F2");
        textoCubo2.transform.position = cubo2.position + Vector3.up;

        linea.SetPosition(0, cubo1.position);
        linea.SetPosition(1, cubo2.position);
    }

    private void ActualizarCaracteristicasLinea()
    {
        linea.startWidth = anchoLinea;
        linea.endWidth = anchoLinea;
        linea.material = materialLinea;
        linea.startColor = colorInicio;
        linea.endColor = colorFin;
    }
}

