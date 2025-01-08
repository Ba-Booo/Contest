using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashJudgmentTutorial : MonoBehaviour
{

    [SerializeField] CameraMoveTutorial cameraMove;
    [SerializeField] PlayerMoveTutorial playerStatus;
    [SerializeField] DashTutorial dashTutorial;

    [SerializeField] float shakePower;
    [SerializeField] float shakeTime;

    void OnTriggerStay2D( Collider2D collision )
    {
     
        if(collision.gameObject.tag == "Enemy" )
        {
            playerStatus.enemyContact = true;
        }


        if(collision.gameObject.tag == "Enemy" && playerStatus.dashing && dashTutorial.inputTutorialDashMove == 3 )
        {

            StartCoroutine( cameraMove.CameraShake( shakePower, shakeTime ) );
            
        }

    }

    void OnTriggerExit2D( Collider2D collision )
    {
        if(collision.gameObject.tag == "Enemy" )
        {
            playerStatus.enemyContact = false;
        }
    }

}
