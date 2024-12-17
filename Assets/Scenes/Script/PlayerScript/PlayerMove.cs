using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float moveX, moveY;

    public float playerSpeed;           //움직임
    public float dashSpeed;

    bool jumpPrevention = false;        //점프
    public float jumpPower;            

    float nowdashGauge;                 //대쉬
    public float maxdashGauge;
    float nextDashTime;
    public float dashRate;


    float mouseAngle;
    public GameObject dashRange;
    public GameObject mousePartical;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        Move();

    }

    void Move()
    {

        //움직임
        if( Input.GetKey( KeyCode.LeftShift ) &&  nowdashGauge >= 0 && nextDashTime <= Time.time )
        {
            Dash();
        }
        else
        {
            moveY = 0;
            moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            Time.timeScale = 1f;
        }

        //대쉬딜레이
        if( Input.GetKeyUp( KeyCode.LeftShift ) && nextDashTime <= Time.time )
        {
            nextDashTime = Time.time + dashRate;
            nowdashGauge = maxdashGauge;
            dashRange.SetActive(false);
            mousePartical.SetActive(false);

        }   

        //점프
        if( Input.GetKeyDown( KeyCode.W ) && jumpPrevention )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
            jumpPrevention = false;
        }

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y + moveY );

    }

    void Dash()
    {

        //대쉬 그래픽
        dashRange.SetActive(true);
        mousePartical.SetActive(true);

   	    mouseAngle = Mathf.Atan2( mousePartical.transform.position.y - transform.position.y, mousePartical.transform.position.x - transform.position.x ) * Mathf.Rad2Deg;
   	    dashRange.transform.rotation = Quaternion.AngleAxis( mouseAngle - 90, Vector3.forward );

        //대쉬움직임
        moveX = Input.GetAxisRaw("Horizontal") * dashSpeed * Time.deltaTime;
        moveY = Input.GetAxisRaw("Vertical") * dashSpeed * Time.deltaTime;
        
        //대쉬 게이지
        if( Input.GetAxisRaw("Horizontal") != 0 | Input.GetAxisRaw("Vertical") != 0 )
        {
            nowdashGauge -= Time.deltaTime * 30;
        }
        else
        {
            nowdashGauge -= Time.deltaTime * 5;
        }

        //시간
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

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
