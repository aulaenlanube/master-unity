using UnityEngine;

public class ArrastrarObjeto : MonoBehaviour
{
    private float coordenadaZ;

    void OnMouseDown()
    {
        // calcular la coordenada Z de la cámara al objeto
        coordenadaZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 ObtenerMouseAsWorldPoint()
    {
        // coordenada del ratón en pantalla
        Vector3 puntoRatonEnPantalla = new Vector3(Input.mousePosition.x, Input.mousePosition.y, coordenadaZ);

        // convertir a posición del mundo
        return Camera.main.ScreenToWorldPoint(puntoRatonEnPantalla);
    }

    void OnMouseDrag()
    {
        // mover el objeto a la nueva posición del ratón manteniendo el desplazamiento
        transform.position = ObtenerMouseAsWorldPoint();
    }
}
