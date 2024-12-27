using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    [SerializeField] GameObject fade;
    Color fadeColor;

    void Start()
    {
        fadeColor = fade.GetComponent<Image>().color;
    }


    public void StartButton()
    {

        StartCoroutine( FadeIn() );

    }


    IEnumerator FadeIn()
    {

        fade.SetActive(true);
        
        while( fadeColor.a < 1f )
        {
            fadeColor.a += Time.deltaTime;

            fade.GetComponent<Image>().color = fadeColor;

            yield return new WaitForSeconds(0.01f);

        }

        SceneManager.LoadScene( "SampleScene" );

    }

}
