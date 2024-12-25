using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField]
    GameObject target;
    PlayerMove pm;

    [SerializeField]
    float cameraMoveSpeed;

    void Start()
    {
        pm = target.GetComponent<PlayerMove>();
    }

    void LateUpdate()
    {

        if( pm.dashing )
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
