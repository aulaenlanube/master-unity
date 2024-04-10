using UnityEngine;
using UnityEngine.AI;

public class ObstaculoMiniShooter : MonoBehaviour
{
    [SerializeField] private float movimientoHorizontal;
    [SerializeField] private float movimientoVertical;
    [SerializeField] private float velocidad;

    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    void Start() 
    { 
        posicionInicial = transform.position;
        posicionFinal = posicionInicial + new Vector3(movimientoHorizontal, movimientoVertical, 0);        
    }

    void Update()
    {        
        transform.position = Vector3.Lerp(posicionInicial, posicionFinal, Mathf.PingPong(Time.time * velocidad, 1.0f));              
    }
}

  

