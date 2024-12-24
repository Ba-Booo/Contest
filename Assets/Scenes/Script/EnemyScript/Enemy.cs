using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // 체력
    [SerializeField]
    int enemyMaxHp;
    [SerializeField]
    int enemyNowHp;

    //쥬금
    [SerializeField]
    Transform dashJudgment;
    [SerializeField]
    GameObject bloodParticle;
    [SerializeField]
    PlayerMove playerStatus;

    // 거리
    float distance;
    [SerializeField]
    float viewingRange;
    [SerializeField]
    float attackRange;
    [SerializeField]
    Transform target;

    // 속도
    [SerializeField]
    float enemySpeed;

    // 공격
    [SerializeField]
    float attackRate;
    float nextAttectTime;
    public GameObject bullat;
    

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        EnemyMove();
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
     
        if(collision.gameObject.tag == "PlayerAttack")
        {

            //피 파티클
            float bloodAngle =  Mathf.Atan2( dashJudgment.position.y - transform.position.y, dashJudgment.position.x - transform.position.x ) * Mathf.Rad2Deg;
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( bloodAngle - 90, Vector3.forward ) );

            //플레이어 궁게이지, 쿨타임
            playerStatus.nextSlowMotionTime = Time.time;
            playerStatus.nowUltimateAttackGauge += 1;

            Destroy(this.gameObject);

        }

    }

}
