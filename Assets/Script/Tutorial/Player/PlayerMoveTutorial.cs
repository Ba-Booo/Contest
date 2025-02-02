using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveTutorial : MonoBehaviour
{

    float moveX, moveY;

    public int tutorial = 0;

    [SerializeField] float playerSpeed;         //움직임     
    [SerializeField] float slowMotionSpeed;
    

    SpriteRenderer sr;                          //에니메이션

    bool jumpPrevention = false;                //점프
    [SerializeField] float jumpPower;

    public float nowSlowMotionGauge;                   //슬로우모션
    public float maxSlowMotionGauge;
    bool slowMotionLimit = true;
    public bool doingSlowMotion;
    public bool wasAttacked;
    public bool enemyContact;
    public bool hitBullet;
    [SerializeField] DashTutorial dashTutorial;

    float mouseAngle;                           //대쉬 방향
    [SerializeField] GameObject attackRange;
    [SerializeField] GameObject mousePartical;

    [SerializeField] float maxDashDistance;                       //대쉬
    public bool dashing;
    public GameObject DashAttackJudgment;
    public AttackedRange attackedRange;

    public float maxUltimateAttackGauge;                    //궁
    public float nowUltimateAttackGauge;
    [SerializeField] GameObject ultimateMousePartical;
    public bool doingUltimateAttack;
    public Vector3 positionUltimate;
    [SerializeField] UltimateJudgmentTutorial ultimateJudgment;
    bool ultimateLight;
    public int enemyCount = 0;

    [SerializeField] UnityEngine.Rendering.Universal.Light2D mainLight;
    [SerializeField] UnityEngine.Rendering.Universal.Light2D myLight;

    Rigidbody2D rb;
    CapsuleCollider2D cldr;
    [SerializeField] CameraMoveTutorial cameraMove;


    void Start()
    {
        
        nowSlowMotionGauge = maxSlowMotionGauge;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {

        Move();
        LightEffect();
        Animation();
        Tutorial();

        //슬로우 모션 후
        if ( ( ( ( !Input.GetKey( KeyCode.LeftShift ) ) && slowMotionLimit ) | nowSlowMotionGauge <= 0 ) && tutorial == 2 )
        {

            AfterSlowMotion();

        } 

        if( hitBullet )
        {
            
            hitBullet = false;
            transform.position = new Vector2( 28f, -0.1f );
            nowSlowMotionGauge = maxSlowMotionGauge;

        }

        //궁
        if( nowUltimateAttackGauge >= maxUltimateAttackGauge && Input.GetKey( KeyCode.Q ) && !doingSlowMotion )
        {

            if( !doingUltimateAttack )
            {
                ultimateMousePartical.transform.position = transform.position;
                StartCoroutine( cameraMove.UltimateAttackCamera() );
            }

            myLight.intensity = Mathf.Lerp( myLight.intensity, 0.8f, 70f * Time.deltaTime );

            UltimateAttack();

        }

        if( enemyCount == 3 )
        {
            enemyCount += 1;
            StartCoroutine( cameraMove.AfterUltimateAttackCamera() );
        }
        else if( enemyCount == 5 )
        {
            
            cameraMove.screenTransition = false;
            ultimateMousePartical.SetActive(false);
            doingUltimateAttack = false;
            dashing = false;
            
            if( ultimateJudgment.contactEnemy )
            {
                mainLight.intensity = 30f;
                ultimateLight = true;
                transform.position = positionUltimate;

                StartCoroutine( cameraMove.CameraShake( 2f, 0.4f ) );
                StartCoroutine( Dash( maxDashDistance / 20) );
                ultimateJudgment.contactEnemy = false;
                
            }

            StopCoroutine( cameraMove.UltimateAttackCamera() );

        }

    }


    void Tutorial()
    {

        switch( tutorial )
        {
            case 0:

                if( transform.position.x >= -1f )
                {
                    tutorial = 1;
                }

                break;
            
            case 3:
             
                if( transform.position.x >= 68f )
                {
                    tutorial = 4;
                }

                break;

        }

    }



    void Move()
    {

        //움직임
        if( Input.GetKey( KeyCode.LeftShift ) && nowSlowMotionGauge >= 0 && slowMotionLimit && !doingUltimateAttack && tutorial == 2 )   //슬로우모션
        {
            SlowMotion();  
        }
        else if( tutorial == 2 )
        {
            moveX = 0f;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

            if( !doingUltimateAttack )
            {
                myLight.intensity = 0f;
            }

            if( wasAttacked )
            {
                StartCoroutine( cameraMove.CameraShake( 1f, 0.1f ) );
                wasAttacked = false;
            }

        }
        else
        {

            moveY = 0;
            moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

            if( !doingUltimateAttack )
            {
                myLight.intensity = 0f;
            }

            if( wasAttacked )
            {
                StartCoroutine( cameraMove.CameraShake( 1f, 0.1f ) );
                wasAttacked = false;
            }

        }

        //점프
        if( Input.GetKeyDown( KeyCode.W ) && jumpPrevention && !doingSlowMotion && tutorial > 0 )
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



    void LightEffect()
    {

        if( doingSlowMotion | doingUltimateAttack )
        {
            mainLight.intensity = Mathf.Lerp( mainLight.intensity, 0.3f, 70f * Time.deltaTime );
        }
        else if( ultimateLight )
        {
            StartCoroutine( UltimateAttackLight() );
        }
        else
        {
            mainLight.intensity = 0.5f;
        }

    }



    void SlowMotion()
    {


        doingSlowMotion = true;

        DashAttackJudgment.SetActive(true);
        DashAttackJudgment.transform.position = transform.position;
        DashAttackJudgment.transform.rotation = attackRange.transform.rotation;

        //그래픽 관련
        attackRange.SetActive(true);
        mousePartical.SetActive(true);
        myLight.intensity = Mathf.Lerp( myLight.intensity, 0.8f, 70f * Time.deltaTime );

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

        nowSlowMotionGauge = maxSlowMotionGauge;
        doingSlowMotion = false;
        attackRange.SetActive(false);
        mousePartical.SetActive(false);

        //판정(벽뚫방지)
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");                 //땅 관련
        RaycastHit2D hit = Physics2D.Raycast( transform.position, mousePartical.transform.position - transform.position, maxDashDistance, groundLayerMask);


        if(enemyContact && !hitBullet && dashTutorial.inputTutorialDashMove == 3 )
        {

            slowMotionLimit = false;

            if( hit && !wasAttacked )
            {
                StartCoroutine( Dash( ( Vector3.Distance( transform.position, hit.point ) ) - 1f ) );
            }
            else if( !wasAttacked )
            {
                StartCoroutine( Dash( maxDashDistance ) );
            }
            else
            {
                StartCoroutine( cameraMove.CameraShake( 1f, 0.1f ) );
                doingSlowMotion = false;
                wasAttacked = false;
            }

        }
        else
        {

            transform.position = new Vector2( 28f, -0.1f );
            
        }

    }


    IEnumerator Dash( float dashDistance )
    {

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

            rb.velocity = new Vector2( mousePartical.transform.position.x - transform.position.x, mousePartical.transform.position.y - transform.position.y ).normalized * 35f * dashDistance;

        }

        yield return new WaitForSeconds(0.15f);

        DashAttackJudgment.SetActive(false);
        attackedRange.dashAttackActivator = false;

        rb.drag = 1f;
        cldr.isTrigger = false;
        doingSlowMotion = false;

    }



    void UltimateAttack()
    {

        cameraMove.screenTransition = true;
        doingUltimateAttack = true;
        dashing = true;

        //시간 조절
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        
        ultimateMousePartical.SetActive(true);

    }

    IEnumerator UltimateAttackLight()
    {
            
        while(mainLight.intensity > 1.1f )
        {
            mainLight.intensity -= 0.2f;
            yield return new WaitForSeconds(0.01f);
        }

        ultimateLight = false;

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