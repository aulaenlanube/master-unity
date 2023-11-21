using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirilla : MonoBehaviour
{
    void Update()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;
    }
}
