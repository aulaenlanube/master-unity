using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public float alcanceDelRayo = 100.0f; // alcance máximo del rayo
    void Update()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayo, out hitInfo, alcanceDelRayo))
            {
                Debug.Log($"El rayo impactó en: {hitInfo.transform.name}");
                // ejemplo1: "destruir" el objeto golpeado
                Destroy(hitInfo.transform.gameObject);

                // ejemplo2: invocar una función en el objeto impactado
                //hitInfo.transform.GetComponent<MoverConFlechas>()?.MoverRandom();
            }
            else Debug.Log("El rayo no impactó en nada dentro del alcance");
        }       
    }
}
