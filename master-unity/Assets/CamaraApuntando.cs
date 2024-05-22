using System.Collections;
using UnityEngine;

public class CamaraApuntando : MonoBehaviour
{   
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float duracion = .1f;
    private GameObject objetoAMover;

    public void CambioCamaraApuntando()
    {
        MiniShooter.instance.CamaraApuntando.enabled = true;
        MiniShooter.instance.CamaraPrincipal.enabled = false;
    }

    public void CambioCamaraPrimeraPersona()
    {
        MiniShooter.instance.CamaraPrincipal.enabled = true;
        MiniShooter.instance.CamaraApuntando.enabled = false;

        //alternamos posición actual de la cámara principal
        MiniShooter.instance.CamaraPrincipal.transform.position = MiniShooter.instance.CamaraApuntando.transform.position;

        IniciarInterpolacion(MiniShooter.instance.CamaraPrincipal.gameObject, MiniShooter.instance.PosCamaraActual());
    }
    public void IniciarInterpolacion(GameObject objeto, Vector3 nuevaPosicion)
    {
        objetoAMover = objeto;
        posicionInicial = objeto.transform.localPosition;
        posicionFinal = nuevaPosicion;
        StartCoroutine(InterpolarPosicion());
    }

 
    private IEnumerator InterpolarPosicion()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {           
            float t = tiempoTranscurrido / duracion;          
            objetoAMover.transform.localPosition = Vector3.Lerp(posicionInicial, posicionFinal, t); 
            tiempoTranscurrido += Time.deltaTime;          
            yield return null;
        }
        objetoAMover.transform.localPosition = posicionFinal;
    }
}
