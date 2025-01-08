using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullat : MonoBehaviour
{

    GameObject target;
    PlayerMoveTutorial playerState;

    DashTutorial dashTutorial;

    //총소리
    AudioSource audioSource;

    void Start()
    {     
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveTutorial>();
        dashTutorial = GameObject.Find("Main Camera").GetComponent<DashTutorial>();

        audioSource.Play();

    }

    void Update()
    {
        audioSource.pitch = Time.timeScale * 2f;

        if( dashTutorial.inputTutorialDashMove == 0 )
        {
            StartCoroutine( TutorialBullatMove() );
        }

        if( dashTutorial.inputTutorialDashMove == 4 )
        {
            Destroy( this.gameObject );
        }

    }

    void OnTriggerEnter2D( Collider2D collision )
    {

        if( collision.gameObject.tag == "Player")
        {
            playerState.hitBullet = true;
        }

    }

    IEnumerator TutorialBullatMove()  
    {
        while( transform.position.x > target.transform.position.x + 1.1f)
        {
            transform.position = Vector3.Lerp( transform.position, new Vector3( target.transform.position.x + 1f, target.transform.position.y, transform.position.z ), Time.deltaTime );
            yield return new WaitForSeconds(0.01f);
        }
    }

}
