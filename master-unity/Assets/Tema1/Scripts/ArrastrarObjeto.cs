using UnityEngine;

public class ArrastrarObjeto : MonoBehaviour
{
    private Vector3 desplazamiento;
    private float coordenadaZ;

    void OnMouseDown()
    {
        // calcular la coordenada Z de la c?mara al objeto
        coordenadaZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // almacenar desplazamiento entre el objeto y el punto de clic
        desplazamiento = gameObject.transform.position - ObtenerMouseAsWorldPoint();
    }

    private Vector3 ObtenerMouseAsWorldPoint()
    {
        // coordenada del rat?n en pantalla
        Vector3 puntoRatonEnPantalla = new Vector3(Input.mousePosition.x, Input.mousePosition.y, coordenadaZ);

        // convertir a posici?n del mundo
        return Camera.main.ScreenToWorldPoint(puntoRatonEnPantalla);
    }

    void OnMouseDrag()
    {
        // mover el objeto a la nueva posici?n del rat?n manteniendo el desplazamiento
        transform.position = ObtenerMouseAsWorldPoint() + desplazamiento;
    }
}
