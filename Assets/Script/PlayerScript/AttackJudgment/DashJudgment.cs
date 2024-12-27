using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJudgment : MonoBehaviour
{

    [SerializeField] CameraMove cameraMove;

    [SerializeField] float shakePower;
    [SerializeField] float shakeTime;

    void OnTriggerEnter2D( Collider2D collision )
    {
     
        if(collision.gameObject.tag == "Enemy" )
        {

            StartCoroutine( cameraMove.CameraShake( shakePower, shakeTime ) );

        }

    }
}
