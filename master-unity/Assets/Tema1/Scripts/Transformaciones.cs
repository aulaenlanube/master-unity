using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformaciones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // rota el objeto 45 grados alrededor del eje Y:
        transform.rotation = Quaternion.Euler(0, 45, 0);
        // incrementa la rotación del objeto en 15 grados alrededor del eje Z
        transform.rotation *= Quaternion.Euler(0, 0, 15);
        // establece la rotación del objeto a su rotación original (sin rotación)
        transform.rotation = Quaternion.identity;



    }

}
