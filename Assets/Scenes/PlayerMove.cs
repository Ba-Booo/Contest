using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float moveX;

    public float playerSpeed;           //움직임
    bool jumpPrevention = false;                //점프
    public float jumpPower;            

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y );

        //점프
        if( Input.GetKeyDown( KeyCode.W ) && jumpPrevention )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
            jumpPrevention = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground")
        {
            jumpPrevention = true;
        }
        
    }

}
