using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{

    //쥬금
    [SerializeField] Transform dashJudgment;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] PlayerMoveTutorial playerStatus;
    [SerializeField] DashTutorial dashTutorial;
    bool ultimateDeath;
    bool deadPosition;

    //빛
    [SerializeField] UnityEngine.Rendering.Universal.Light2D myLight;

    AudioSource audioSource;

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveTutorial>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        //궁 쥬금
        if( !playerStatus.doingUltimateAttack && ultimateDeath )
        {
            //피 파티클
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( Random.Range( 0f, 360f ), Vector3.forward ) );

            Destroy(this.gameObject);

        }

        if( playerStatus.doingSlowMotion | playerStatus.doingUltimateAttack )
        {
            myLight.intensity = Mathf.Lerp( myLight.intensity, 1f, 70f * Time.deltaTime );
        }
        else
        {
            myLight.intensity = 0f;
        }


    }

    void OnTriggerStay2D( Collider2D collision )
    {
     
        if(collision.gameObject.name == "DashAttackJudgment" && playerStatus.dashing && dashTutorial.inputTutorialDashMove == 3 )
        {

            //피 파티클
            float bloodAngle =  Mathf.Atan2( dashJudgment.position.y - transform.position.y, dashJudgment.position.x - transform.position.x ) * Mathf.Rad2Deg;
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( bloodAngle - 90, Vector3.forward ) );

            //플레이어 궁게이지, 쿨타임
            playerStatus.nowUltimateAttackGauge += 1;
            playerStatus.dashing = false;
            playerStatus.tutorial = 3;

            Destroy(this.gameObject);

        }

        if(collision.gameObject.name == "UltimateAttackMouse")
        {

            ultimateDeath = true;

            if( !deadPosition )
            {
                audioSource.Play();
                playerStatus.positionUltimate = transform.position;
                deadPosition = true;

            }

        }

    }

}