using UnityEngine;

public class CamaraDosCubos : MonoBehaviour
{
    public Transform cubo1;
    public Transform cubo2;
    public float distanciaMinima;

    // Update is called once per frame
    void Update()
    {
        Vector3 puntoMedio = (cubo1.position + cubo2.position) / 2;
        float distancia = (cubo1.position - cubo2.position).magnitude;
        distancia = Mathf.Max(distancia, distanciaMinima);
        transform.position = puntoMedio - transform.forward * distancia;        
    }
}
