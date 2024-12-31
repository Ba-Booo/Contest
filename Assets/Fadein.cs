using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fadein : MonoBehaviour
{

    [SerializeField] GameObject fade;
    Color fadeColor;

    void Start()
    {
        fadeColor = fade.GetComponent<Image>().color;
        StartCoroutine( FadeIn() );
    }

    IEnumerator FadeIn()
    {

        fade.SetActive(true);
        
        while( fadeColor.a > 0.01f )
        {
            fadeColor.a -= Time.deltaTime;

            fade.GetComponent<Image>().color = fadeColor;

            yield return new WaitForSeconds(0.01f);

        }
        
        

    }
}
