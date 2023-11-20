using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoverConFlechasPersonalizable : MonoBehaviour
{
    private const int VELOCIDAD_POR_DEFECTO = 10;
    public float velocidad = 10f;
    public float duracionRalentizacion = 5;
    public KeyCode arriba;
    public KeyCode abajo;
    public KeyCode izquierda;
    public KeyCode derecha;
    public KeyCode duplicarVelocidad;
    public KeyCode dividirVelocidad;


    void Update()
    {
        float despHorizontal = 0;
        float despVertical = 0;
        if (Input.GetKey(arriba)) despVertical = 1;
        if (Input.GetKey(abajo)) despVertical = -1;
        if (Input.GetKey(izquierda)) despHorizontal = -1;
        if (Input.GetKey(derecha)) despHorizontal = 1;

        if (Input.GetKeyDown(duplicarVelocidad)) velocidad*=2;
        if (Input.GetKeyDown(dividirVelocidad)) velocidad/=2;

        // calcular el vector de desplazamiento
        Vector3 desplazamiento = new Vector3(despHorizontal, 0, despVertical);

        // mover el GameObject
        transform.Translate(desplazamiento * velocidad * Time.deltaTime);
    }

    public void RalentizarCubo()
    {
        StartCoroutine(Ralentizar());
    }

    private IEnumerator Ralentizar()
    {
        velocidad /= 2;
        yield return new WaitForSeconds(duracionRalentizacion);
        velocidad = VELOCIDAD_POR_DEFECTO;
    }
}
