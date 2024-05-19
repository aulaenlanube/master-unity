using UnityEngine;

public class ArmaMiniShooter : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !MiniShooter.instance.EstaCorriendo())
        {
            OrientarYPosicionarHaciaCentro();
        }


    }

    void OrientarYPosicionarHaciaCentro()
    {
        // Calcula el punto central de la pantalla
        Vector3 puntoCentroPantalla = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);

        // Convierte el punto central de la pantalla a una posición en el mundo
        Vector3 posicionEnElMundo = Camera.main.ScreenToWorldPoint(puntoCentroPantalla);

        // Calcula la dirección desde el GameObject hacia la posición en el plano cercano
        Vector3 direccion = posicionEnElMundo - transform.position;

        // Orienta el GameObject hacia la dirección calculada
        direccion.y = 0; // Ignora la componente y para mantener la orientación en el plano horizontal
        Quaternion rotacionHaciaCentro = Quaternion.LookRotation(direccion);
        transform.rotation = rotacionHaciaCentro;

        // Mueve el GameObject a la posición en el plano cercano
        transform.position = posicionEnElMundo;
    }
}
