using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject target;
    PlayerMove pm;

    public float cameraMoveSpeed;

    void Start()
    {
        pm = target.GetComponent<PlayerMove>();
    }

    void LateUpdate()
    {

        if( pm.dashAttact )
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 100f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, cameraMoveSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -30);

    }

}
