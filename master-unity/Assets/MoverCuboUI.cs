using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverCuboUI : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5.0f;
    public float duracion = 1f;

    public Button botonArriba;
    public Button botonAbajo;
    public Button botonIzquierda;
    public Button botonDerecha;

    private float despHorizontal = 0;
    private float despVertical = 0;

    void Start()
    {
        botonArriba?.onClick.AddListener(Arriba);
        botonAbajo?.onClick.AddListener(Abajo);
        botonIzquierda?.onClick.AddListener(Izquierda);
        botonDerecha?.onClick.AddListener(Derecha);
    }

    private void Arriba()
    {
        despVertical = 1;
        MoverLerp();
    }
    private void Abajo()
    {
        despVertical = -1;
        MoverLerp();
    }
    private void Izquierda()
    {
        despHorizontal = -1;
        MoverLerp();
    }
    private void Derecha()
    {
        despHorizontal = 1;
        MoverLerp();
    }
    private void Mover()
    {
        // calcular el vector de desplazamiento
        Vector3 desplazamiento = new Vector3(despHorizontal, 0, despVertical);

        // mover el GameObject
        objetivo.Translate(desplazamiento * velocidad);

        //reiniciamos desplazamiento
        despHorizontal = 0;
        despVertical = 0;
    }
    private void MoverLerp()
    {
        Vector3 posicionObjetivo = objetivo.position + new Vector3(despHorizontal, 0, despVertical)*velocidad;
        StartCoroutine(MoverHaciaPosicion(posicionObjetivo, duracion));
    }

    IEnumerator MoverHaciaPosicion(Vector3 destino, float duracion)
    {
        float tiempoInicio = Time.time;
        Vector3 posicionInicial = objetivo.position;
        float fraccion = 0f;
        while (fraccion < 1f)
        {            
            fraccion = (Time.time - tiempoInicio) / duracion;

            float fraccionSmooth = Mathf.SmoothStep(0f, 1f, fraccion);
            objetivo.position = Vector3.Lerp(posicionInicial, destino, fraccionSmooth);
            yield return null;
        }
        objetivo.position = destino;

        //reiniciamos desplazamiento
        despHorizontal = 0;
        despVertical = 0;
    }

}
