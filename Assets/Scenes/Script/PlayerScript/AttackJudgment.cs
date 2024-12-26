using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJudgment : MonoBehaviour
{


    [SerializeField] CameraMove cameraMove;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] float shakePower;
    [SerializeField] float shakeTime;

    public bool contactEnemy;


    void OnTriggerStay2D( Collider2D collision )
    {
     
        if(collision.gameObject.tag == "Enemy" )
        {
            contactEnemy = true;
        }
        else
        {
            contactEnemy = false;
        }

    }

    void OnTriggerEnter2D( Collider2D collision )
    {
     
        if(collision.gameObject.tag == "Enemy" )
        {

            StartCoroutine( cameraMove.CameraShake( shakePower, shakeTime ) );

        }

    }
}
