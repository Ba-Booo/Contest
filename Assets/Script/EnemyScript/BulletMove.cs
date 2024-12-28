using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //윰직임
    [SerializeField]
    float bullatSpeed;
    GameObject target;
    Rigidbody2D bullatRB;

    //딜
    PlayerUI playerHP;
    PlayerMove playerState; 

    //총소리
    AudioSource audioSource;

    void Start()
    {     
        audioSource = GetComponent<AudioSource>();
        bullatRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>();
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        Vector2 moveDir = (target.transform.position - transform.position).normalized * bullatSpeed;
        bullatRB.velocity = moveDir;

        audioSource.Play();

        Destroy(this.gameObject, 2);

    }

    void Update()
    {
        audioSource.pitch = Time.timeScale * 2f;
    }


    void OnTriggerEnter2D( Collider2D collision )
    {

        //딜
        if( collision.gameObject.tag == "Player" && !playerState.dashing && playerState.doingSlowMotion )
        {
            
            playerHP.playerNowHP -= 1;
            playerState.wasAttacked = true;
            playerState.nowSlowMotionGauge = 0;
            Destroy( this.gameObject);

        }
        else if( collision.gameObject.tag == "Player" && !playerState.dashing  )
        {
            playerState.wasAttacked = true;
            playerHP.playerNowHP -= 1;
            Destroy( this.gameObject);
        }

        if( collision.gameObject.tag == "Ground" )
        {

            Destroy( this.gameObject );

        }
        
    }

}