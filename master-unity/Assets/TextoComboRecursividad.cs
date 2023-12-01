using UnityEngine;
using UnityEngine.UI;

public class TextoComboRecursividad : MonoBehaviour
{
    private void OnEnable()
    {
        JuegoRecursividad.comboActualizado += ActualizarCombo;
    }

    private void OnDisable()
    {
        JuegoRecursividad.comboActualizado -= ActualizarCombo;
    }

    private void ActualizarCombo(int combo)
    {        
        GetComponent<Text>().text = $"Combo:{combo}";
    }
}
