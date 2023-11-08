using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventosRaton : MonoBehaviour
{
    public bool multipleRayCast = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                        Debug.Log($"Has hecho click en: {obj.name}");
                    }
                }
            } 
        }
    }
}
