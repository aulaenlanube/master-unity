using System.Collections;
using UnityEngine;

public class DetectarImpactos : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                PuntuacionDiana puntuacionObjetivo = hit.collider.GetComponent<PuntuacionDiana>();
                puntuacionObjetivo?.Impacto();
                StartCoroutine(GirarDiana(hit.collider.gameObject.transform.parent.gameObject));
            }
            else Debug.Log("Mejora tu punteria");
        }
    }

    IEnumerator GirarDiana(GameObject diana)
    {
        for(int i = 0; i<18; i++)
        {
            diana.transform.Rotate(0f, 100f, 0f);
            yield return null;
        }
    }
}

