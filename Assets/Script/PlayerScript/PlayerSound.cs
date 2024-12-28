using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    bool isMoving;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

        audioSource.pitch = Time.timeScale;

        if( Input.GetAxisRaw("Horizontal") != 0 && isMoving )
        {
            if( !audioSource.isPlaying )
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnCollisionStay2D( Collision2D collision )
    {

        if( collision.gameObject.tag == "Ground" )
        {
            isMoving = true;
        }
        
    }

    void OnCollisionExit2D( Collision2D collision )
    {

        if(collision.gameObject.tag == "Ground" )
        {
            isMoving = false;
        }
        
    }

}
