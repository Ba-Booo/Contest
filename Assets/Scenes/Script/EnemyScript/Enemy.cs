using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // 체력
    public int maxHp;
    public int nowHp;

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
}
