using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // 체력
    [SerializeField] int enemyMaxHp;
    [SerializeField] int enemyNowHp;

    //쥬금
    [SerializeField] Transform dashJudgment;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] PlayerMove playerStatus;
    bool ultimateDeath;
    bool deadPosition;

    // 거리
    float distance;
    [SerializeField] float viewingRange;
    [SerializeField] float attackRange;
    [SerializeField] Transform target;

    // 속도
    [SerializeField] float enemySpeed;

    // 공격
    [SerializeField]
    float attackRate;
    float nextAttectTime;
    public GameObject bullat;

    //빛
    [SerializeField] UnityEngine.Rendering.Universal.Light2D myLight;

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        EnemyMove();

        //궁 쥬금
        if( !playerStatus.doingUltimateAttack && ultimateDeath )
        {
            //피 파티클
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( Random.Range( 0f, 360f ), Vector3.forward ) );

            //플레이어 쿨타임
            playerStatus.nextSlowMotionTime = Time.time;

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

    void EnemyMove()
    {

        distance = Vector2.Distance( target.position, transform.position );

        //ai
        if( distance <= viewingRange && distance >= attackRange )
        {
            transform.position = Vector2.MoveTowards( transform.position, target.position, enemySpeed * Time.deltaTime );
        }
        else if( distance <= attackRange && nextAttectTime <= Time.time )
        {
            Instantiate( bullat, transform.position, Quaternion.identity);
            nextAttectTime = Time.time + attackRate;
        }

    }

    void OnTriggerEnter2D( Collider2D collision )
    {
     
        if(collision.gameObject.name == "DashAttackJudgment")
        {

            //피 파티클
            float bloodAngle =  Mathf.Atan2( dashJudgment.position.y - transform.position.y, dashJudgment.position.x - transform.position.x ) * Mathf.Rad2Deg;
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( bloodAngle - 90, Vector3.forward ) );

            //플레이어 궁게이지, 쿨타임
            playerStatus.nextSlowMotionTime = Time.time;
            playerStatus.nowUltimateAttackGauge += 1;

            Destroy(this.gameObject);

        }

        if(collision.gameObject.name == "UltimateAttackMouse")
        {

            ultimateDeath = true;

            if( !deadPosition )
            {

                playerStatus.positionUltimate = transform.position;
                deadPosition = true;

            }

        }

    }

}