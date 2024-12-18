using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float moveX, moveY;

    public float playerSpeed;           //움직임
    public float slowMotionSpeed;

    bool jumpPrevention = false;        //점프
    public float jumpPower;            

    float nowSlowMotionGauge;           //슬로우모션
    public float maxSlowMotionGauge;
    float nextSlowMotionTime;
    public float slowMotionRate;

    float mouseAngle;                   
    public GameObject attackRange;
    public GameObject mousePartical;

    public float dashSpeed;             //대쉬

    Rigidbody2D rb;

    void Start()
    {
        nowSlowMotionGauge = maxSlowMotionGauge;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();

    }

    void Move()
    {

        //움직임
        if( Input.GetKey( KeyCode.LeftShift ) &&  nowSlowMotionGauge >= 0 && nextSlowMotionTime <= Time.time )      //슬로우모션
        {
            SlowMotion();
        }
        else
        {
            moveY = 0;
            moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            Time.timeScale = 1f;
        }

        //슬로우 모션 후
        if( ( Input.GetKeyUp( KeyCode.LeftShift ) && nextSlowMotionTime <= Time.time ) | nowSlowMotionGauge <= 0)
        {
            nextSlowMotionTime = Time.time + slowMotionRate;
            nowSlowMotionGauge = maxSlowMotionGauge;
            attackRange.SetActive(false);
            mousePartical.SetActive(false);
            Dash();

        }   

        //점프
        if( Input.GetKeyDown( KeyCode.W ) && jumpPrevention )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
            jumpPrevention = false;
        }

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y + moveY );

    }

    void SlowMotion()
    {

        //그래픽 관련
        attackRange.SetActive(true);
        mousePartical.SetActive(true);

        //방향
   	    mouseAngle = Mathf.Atan2( mousePartical.transform.position.y - transform.position.y, mousePartical.transform.position.x - transform.position.x ) * Mathf.Rad2Deg;
   	    attackRange.transform.rotation = Quaternion.AngleAxis( mouseAngle - 90, Vector3.forward );

        //움직임
        moveX = Input.GetAxisRaw("Horizontal") * slowMotionSpeed * Time.deltaTime;
        moveY = Input.GetAxisRaw("Vertical") * slowMotionSpeed * Time.deltaTime;
        
        //느려짐 시전시간
        if( Input.GetAxisRaw("Horizontal") != 0 | Input.GetAxisRaw("Vertical") != 0 )
        {
            nowSlowMotionGauge -= Time.deltaTime * 30;
        }
        else
        {
            nowSlowMotionGauge -= Time.deltaTime * 5;
        }

        //시간 조절
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }

    void Dash()
    {
        rb.velocity = (mousePartical.transform.position - transform.position).normalized * dashSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground")
        {
            jumpPrevention = true;
        }
        else
        {
            jumpPrevention = false;
        }
        
    }

}
