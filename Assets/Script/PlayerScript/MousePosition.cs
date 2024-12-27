using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    Vector3 screenPosition;
    Vector3 mouse;



    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 10;

        mouse = Camera.main.ScreenToWorldPoint( screenPosition );

        transform.position = mouse;
    }
}
