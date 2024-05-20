using System.Collections;
using UnityEngine;

public class CamaraApuntando : MonoBehaviour
{
    public void CambioCamaraApuntando()
    {
        MiniShooter.instance.CamaraApuntando.enabled = true;
        MiniShooter.instance.CamaraPrincipal.enabled = false;
    }

    public void CambioCamaraPrimeraPersona()
    {
        MiniShooter.instance.CamaraPrincipal.enabled = true;
        MiniShooter.instance.CamaraApuntando.enabled = false;
    }


}
