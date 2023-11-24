using UnityEngine;
using UnityEngine.UI;

public class DianaDisparos : MonoBehaviour
{
    private void OnEnable()
    {
        Diana.disparosActualizados += ActualizarDisparos;
    }

    private void OnDisable()
    {
        Diana.puntuacionActualizada -= ActualizarDisparos;
    }

    private void ActualizarDisparos(int disparos)
    {
        GetComponent<Text>().text = $"Disparos restantes: {disparos}";
    }
}
