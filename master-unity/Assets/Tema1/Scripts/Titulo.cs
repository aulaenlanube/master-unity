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
        //ejemplos n�meros enteros
        textoUI.text += "Ejemplos n�meros enteros:\n";        

        int num1 = 1;
        int num2 = 2;
        textoUI.text += num1 + num2 + "\n";     // 3

        int numeroMaximo = int.MaxValue;        // 2147483647
        int numeroMinimo = int.MinValue;        //-2147483648
        uint numeroMaximoU = uint.MaxValue;     // 4294967295
        uint numeroMinimoU = uint.MinValue;     //0

        textoUI.text += "N�mero m�ximo int: " + numeroMaximo +"\n";      //  2147483647
        textoUI.text += "N�mero m�nimo int: " + numeroMinimo + "\n";     // -2147483648
        textoUI.text += "N�mero m�ximo uint: " + numeroMaximoU + "\n";   //  4294967295
        textoUI.text += "N�mero m�nimo uint: " + numeroMinimoU + "\n";   //  0

        int max = numeroMaximo + 1;     // causar� un overflow  
        int min = numeroMinimo - 1;     // causar� un underflow
        uint maxU = numeroMaximoU + 1;  // causar� un overflow  
        uint minU = numeroMinimoU - 1;  // causar� un underflow

        textoUI.text += "N�mero m�ximo int + 1: " + numeroMaximo + "\n";     //  2147483647
        textoUI.text += "N�mero m�nimo int - 1: " + numeroMinimo + "\n";     // -2147483648
        textoUI.text += "N�mero m�ximo uint + 1: " + maxU + "\n"; ;         //  0
        textoUI.text += "N�mero m�nimo uint - 1: " + minU + "\n";           // 4294967295

        // ejemplos n�meros reales
        textoUI.text += "\nEjemplos n�meros reales:\n";

        float num3 = 0.5f;
        float num4 = 3f;        
        textoUI.text += num3 + num4 + "\n";  // 3.5

        float floatNumero = 1.123456789f;           // 6-9 d�gitos de precisi�n aprox
        double doubleNumero = 1.1234567890123456;   // 15-17 d�gitos de precisi�n aprox

        textoUI.text += "N�mero float: " + floatNumero + "\n";     // 1.123457
        textoUI.text += "N�mero double: " + doubleNumero + "\n";   // 1.12345678901235
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
