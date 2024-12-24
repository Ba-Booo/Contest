using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float moveX, moveY;

    [SerializeField]
    float playerSpeed;                          //움직임
    [SerializeField]
    float slowMotionSpeed;

    SpriteRenderer sr;                          //에니메이션

    bool jumpPrevention = false;                //점프
    [SerializeField]
    float jumpPower;            

    float nowSlowMotionGauge;                   //슬로우모션
    public float maxSlowMotionGauge;
    public float nextSlowMotionTime;
    public float slowMotionRate;
    bool slowMotionLimit = true;

    float mouseAngle;                           //대쉬 방향
    public GameObject attackRange;
    public GameObject mousePartical;

    [SerializeField]                            //대쉬
    float maxDashDistance;
    public bool dashing;
    public GameObject DashAttackJudgment;

    Rigidbody2D rb;
    CapsuleCollider2D cldr;

    void Start()
    {
        
        nowSlowMotionGauge = maxSlowMotionGauge;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<CapsuleCollider2D>();
       
    }

    void Update()
    {
        
        Debug.DrawRay(transform.position, mousePartical.transform.position - transform.position, Color.red);
        Move();
        Animation();

        //슬로우 모션 후
        if( ( ( Input.GetKeyUp( KeyCode.LeftShift ) && nextSlowMotionTime <= Time.time ) | nowSlowMotionGauge <= 0 ) && slowMotionLimit )
        {

            AfterSlowMotion();

        }  

        //대쉬 겹침 방지
        if( !Input.GetKey( KeyCode.LeftShift ) )
        {
            slowMotionLimit = true;
        }

    }



    void Move()
    {

        //움직임
        if( Input.GetKey( KeyCode.LeftShift ) &&  nowSlowMotionGauge >= 0 && nextSlowMotionTime <= Time.time && slowMotionLimit )      //슬로우모션
        {

            SlowMotion();  

        }
        else
        {

            moveY = 0;
            moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

        }

         

        //점프
        if( Input.GetKeyDown( KeyCode.W ) && jumpPrevention )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
            jumpPrevention = false;
        }

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y + moveY );

    }



    void Animation()
    {

        switch( Input.GetAxisRaw("Horizontal") )
        {

            case -1:

                sr.flipX = true;
                break;

            case 1:

                sr.flipX = false;
                break; 

        }

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
            nowSlowMotionGauge -= Time.deltaTime * 50;
        }
        else
        {
            nowSlowMotionGauge -= Time.deltaTime * 5;
        }

        //시간 조절
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }


    void AfterSlowMotion()
    {
        slowMotionLimit = false;
        nextSlowMotionTime = Time.time + slowMotionRate;
        nowSlowMotionGauge = maxSlowMotionGauge;
        attackRange.SetActive(false);
        mousePartical.SetActive(false);

        //판정(벽뚫방지)
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");                 //땅 관련
        RaycastHit2D hit = Physics2D.Raycast( transform.position, mousePartical.transform.position - transform.position, maxDashDistance, groundLayerMask);

        if( hit )
        {
            StartCoroutine( Dash( ( Vector3.Distance( transform.position, hit.point ) ) - 1f ) );
        }
        else
        {
            StartCoroutine( Dash( maxDashDistance ) );
        }
        
    }

    IEnumerator Dash( float dashDistance )
    {

        DashAttackJudgment.SetActive(true);
        DashAttackJudgment.transform.position = transform.position;
        DashAttackJudgment.transform.rotation = attackRange.transform.rotation;

        rb.drag = ( 35f * dashDistance ) / 8f;
        cldr.isTrigger = true;
        dashing = true;

        //벽뚫방지
        if( jumpPrevention && dashDistance != maxDashDistance )
        {

            if( 180f <= attackRange.transform.eulerAngles.z && attackRange.transform.eulerAngles.z < 360f )
            {
                rb.velocity = Vector2.right * 20f * dashDistance;
            }
            else
            {
                rb.velocity = Vector2.left * 20f * dashDistance;
            }
    
        }
        else
        {

            rb.velocity = ( mousePartical.transform.position - transform.position ).normalized * 35f * dashDistance;

        }

        yield return new WaitForSeconds(0.15f);

        DashAttackJudgment.SetActive(false);

        rb.drag = 1f;
        cldr.isTrigger = false;
        dashing = false;

    }

    void OnCollisionStay2D( Collision2D collision )
    {

        //점프관련
        if(collision.gameObject.tag == "Ground" | collision.gameObject.tag == "Enemy")
        {
            jumpPrevention = true;
        }
        
    }

    void OnCollisionExit2D( Collision2D collision )
    {

        //점프관련
        if(collision.gameObject.tag == "Ground" | collision.gameObject.tag == "Enemy")
        {
            jumpPrevention = false;
        }
        
    }

    void OnTriggerEnter2D( Collider2D collision )
    {
        
        if(collision.gameObject.tag == "Ground")
        {
            cldr.isTrigger = false;
        }

    }

}