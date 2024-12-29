using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullat : MonoBehaviour
{

    [SerializeField] GameObject target;
    DashTutorial dashTutorial;

    //총소리
    AudioSource audioSource;

    void Start()
    {     
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
        dashTutorial = GameObject.Find("Main Camera").GetComponent<DashTutorial>();

        audioSource.Play();
    }

    void Update()
    {
        audioSource.pitch = Time.timeScale * 2f;
        StartCoroutine( TutorialBullatMove() );

        if( dashTutorial.inputTutorialDashMove == 4 )
        {
            Destroy( this.gameObject );
        }

    }

    IEnumerator TutorialBullatMove()
    {
        while( transform.position.x > target.transform.position.x + 1f)
        {
            transform.position = Vector3.Lerp( transform.position, target.transform.position, Time.deltaTime );
            yield return new WaitForSeconds(0.01f);

        }
    }

}
