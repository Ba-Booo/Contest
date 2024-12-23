using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // 체력
    public int maxHp;
    public int nowHp;

    //쥬금
    public GameObject bloodParticle;

    // 거리
    float distance;
    public float viewingRange;
    public float attackRange;
    public Transform target;

    // 속도
    public float enemySpeed;

    // 공격
    public float attackRate;
    float nextAttectTime;
    public GameObject bullat;
    

    void Start()
    {

    }

    void Update()
    {
        EnemyMove();

    }

    void EnemyMove()
    {

        distance = Vector2.Distance( target.position, transform.position );

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

        Debug.Log(transform.eulerAngles.x);
     
        if(collision.gameObject.tag == "PlayerAttack")
        {
            Debug.Log("내가 고자라니");
            float bloodAngle =  Mathf.Atan2( target.position.y - transform.position.y, target.position.x - transform.position.x ) * Mathf.Rad2Deg;
            Instantiate( bloodParticle, transform.position, Quaternion.AngleAxis( bloodAngle + 90, Vector3.forward ) );
            Destroy(this.gameObject);
        }

    }

}
