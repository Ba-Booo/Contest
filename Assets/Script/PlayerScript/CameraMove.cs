using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] PlayerMove pm;

    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;

    [SerializeField]
    float cameraMoveSpeed;

    bool cameraShaking;

    Camera camera;
    float ultimateCameraZoom;

    AudioSource audioSource;

    void Start()
    {
        camera = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if( Time.timeScale != 1f )
        {
            audioSource.pitch = Mathf.Lerp( audioSource.pitch, Time.timeScale, 30f * Time.deltaTime );
        }
        else
        {
            audioSource.pitch = Mathf.Lerp( audioSource.pitch, 1f, 30f * Time.deltaTime );
        }

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

        float xClamp = Mathf.Clamp( transform.position.x, xRange.x, xRange.y );
        float yClamp = Mathf.Clamp( transform.position.y, yRange.x, yRange.y );

        transform.position = new Vector3(xClamp, yClamp, -10f);

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

    public IEnumerator UltimateAttackCamera()
    {

        ultimateCameraZoom = 0f;

        while( camera.orthographicSize > 2f)
        {
            camera.orthographicSize = ( -( 1f / 16f ) * ( Mathf.Pow( ultimateCameraZoom - 3f, 2) ) ) + 15f;
            ultimateCameraZoom += 0.3f;
            yield return new WaitForSeconds(0.001f);
        }

        camera.orthographicSize = 13f;
        pm.nowUltimateAttackGauge = 0;
        pm.doingUltimateAttack = false;
        

    }

}