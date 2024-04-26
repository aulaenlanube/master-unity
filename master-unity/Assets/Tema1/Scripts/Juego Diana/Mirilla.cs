using UnityEngine;
using UnityEngine.SceneManagement;

public class Mirilla : MonoBehaviour
{
    void Update()
    {      

        GetComponent<RectTransform>().position = Input.mousePosition;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Application.Quit();

        }
    }
}
