using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titulo : MonoBehaviour
{
    public Text textoUI;
    public float velocidad = 5f;
    public GameObject cubo1;
    public GameObject cubo2;
    public GameObject cubo3;

    // Start is called before the first frame update
    void Start()
    {
        //ejemplos números enteros
        textoUI.text += "Ejemplos números enteros:\n";        

        int num1 = 1;
        int num2 = 2;
        textoUI.text += num1 + num2 + "\n";     // 3

        int numeroMaximo = int.MaxValue;        // 2147483647
        int numeroMinimo = int.MinValue;        //-2147483648
        uint numeroMaximoU = uint.MaxValue;     // 4294967295
        uint numeroMinimoU = uint.MinValue;     //0

        textoUI.text += "Número máximo int: " + numeroMaximo +"\n";      //  2147483647
        textoUI.text += "Número mínimo int: " + numeroMinimo + "\n";     // -2147483648
        textoUI.text += "Número máximo uint: " + numeroMaximoU + "\n";   //  4294967295
        textoUI.text += "Número mínimo uint: " + numeroMinimoU + "\n";   //  0

        int max = numeroMaximo + 1;     // causará un overflow  
        int min = numeroMinimo - 1;     // causará un underflow
        uint maxU = numeroMaximoU + 1;  // causará un overflow  
        uint minU = numeroMinimoU - 1;  // causará un underflow

        textoUI.text += "Número máximo int + 1: " + numeroMaximo + "\n";     //  2147483647
        textoUI.text += "Número mínimo int - 1: " + numeroMinimo + "\n";     // -2147483648
        textoUI.text += "Número máximo uint + 1: " + maxU + "\n"; ;         //  0
        textoUI.text += "Número mínimo uint - 1: " + minU + "\n";           // 4294967295

        // ejemplos números reales
        textoUI.text += "\nEjemplos números reales:\n";

        float num3 = 0.5f;
        float num4 = 3f;        
        textoUI.text += num3 + num4 + "\n";  // 3.5

        float floatNumero = 1.123456789f;           // 6-9 dígitos de precisión aprox
        double doubleNumero = 1.1234567890123456;   // 15-17 dígitos de precisión aprox

        textoUI.text += "Número float: " + floatNumero + "\n";     // 1.123457
        textoUI.text += "Número double: " + doubleNumero + "\n";   // 1.12345678901235
    }

    void Update()
    {
        // rotamos los cubos 1 y 2 
        cubo1.transform.Rotate(0, velocidad * Time.deltaTime, 0);
        cubo2.transform.Rotate(0, velocidad * Time.deltaTime, 0);   
        
    }
    void FixedUpdate()
    {
        // rotamos cubo3
        cubo3.transform.Rotate(0, velocidad * Time.deltaTime, 0);
    }
}
