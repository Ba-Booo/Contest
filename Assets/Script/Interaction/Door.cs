using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{


    [SerializeField] GameObject fade;
    Color fadeColor;

    void Start()
    {
        fadeColor = fade.GetComponent<Image>().color;
    }

    IEnumerator FadeIn()
    {
        
        while( fadeColor.a < 1f )
        {
            fadeColor.a += Time.deltaTime;

            fade.GetComponent<Image>().color = fadeColor;

            yield return new WaitForSeconds(0.01f);

        }

        SceneManager.LoadScene( "SampleScene" );

    }

    void OnTriggerStay2D( Collider2D collision )
    {

        if(collision.gameObject.tag == "Player" && Input.GetKey( KeyCode.E ) )
        {
            StartCoroutine( FadeIn() );
        }
        
    }


}
