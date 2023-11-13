using UnityEditor;
using UnityEngine;

public class EventosRaton : MonoBehaviour
{
    public bool multipleRayCast = false;
    public float longitudRayo = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {   
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

            //raycast múltiple
            if(multipleRayCast)
            {
                // lanzamos un raycast que detecte todos los colliders en la dirección del rayo
                RaycastHit[] impactos = Physics.RaycastAll(rayo, Mathf.Infinity);

                // ordenamos de menor a mayor distancia
                System.Array.Sort(impactos,(impacto1, impacto2) => impacto1.distance.CompareTo(impacto2.distance));

                foreach (RaycastHit impacto in impactos)
                {
                    GameObject obj = impacto.collider.gameObject;
                    Debug.Log($"El rayo impactó en: {obj.name}, distancia: {impacto.distance}");
                }
            }
            //raycast simple
            else
            {
                RaycastHit hitInfo;

                if (Physics.Raycast(rayo, out hitInfo))
                {
                    GameObject obj = hitInfo.collider.gameObject;
                    if (obj != null)
                    {
                        //Debug.Log($"Has hecho click en: {obj.name}");
                        Debug.DrawRay(rayo.origin, rayo.direction * longitudRayo, Color.red);

                        //mal planteado
                        Vector3 posicionRaton = Input.mousePosition;
                        posicionRaton.z = 0;
                        Vector3 direccion = posicionRaton - Camera.main.transform.position;
                        Debug.DrawRay(Camera.main.transform.position, direccion * 10f, Color.green);

                        //bien planteado de forma más abreviada
                        
                        Vector3 direccion2 = (hitInfo.point - Camera.main.transform.position).normalized;
                        //movemos un poco arriba
                        Vector3 posCamaraUnPocoArriba = Camera.main.transform.position;
                        posCamaraUnPocoArriba.y += 0.1f;
                        Debug.DrawRay(posCamaraUnPocoArriba, direccion2 * longitudRayo, Color.green);

                        // calculas punto del ratón en pantalla, respecto al mundo
                        Vector3 direccionValida = (hitInfo.point - Camera.main.transform.position).normalized;
                        Debug.DrawRay(Camera.main.transform.position, direccionValida * 10f, Color.green);
                        Debug.Log("click en pantalla: "+Input.mousePosition+" posicion en mundo debe ser 0,0,0"+Camera.main.ScreenToWorldPoint(Input.mousePosition));


                        //Ampliación en el Tema 2 donde veremos el cálculo de la intersección donde impacta sobre el objeto 3D

                    }
                }
            } 
        }
    }
}
