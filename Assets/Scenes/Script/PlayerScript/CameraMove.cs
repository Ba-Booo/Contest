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

    bool cameraShaking;

    void Start()
    {
        pm = target.GetComponent<PlayerMove>();
    }

    void LateUpdate()
    {

        if( pm.dashing && !cameraShaking )
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 100f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, cameraMoveSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -30);

    }

    public IEnumerator CameraShake( float power, float shakeTime )
    {

        cameraShaking = true;


        while( shakeTime >= 0f )
        {

            transform.position = transform.position + ( Random.insideUnitSphere * power );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            
            shakeTime -= Time.deltaTime;

            yield return null;

        }

        cameraShaking = false;

    }

}