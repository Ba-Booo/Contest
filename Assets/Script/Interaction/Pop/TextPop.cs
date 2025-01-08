using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPop : MonoBehaviour
{

    [SerializeField]
    Transform target;

    [SerializeField]
    float activationDistance;

    Vector3 originalPosition;
    Vector3 hiddenPosition;

    Color textColor;

    void Start()
    {
        textColor = this.GetComponent<TMP_Text>().color;
        originalPosition = transform.position;
        hiddenPosition = new Vector3( transform.position.x, transform.position.y -1f, transform.position.z );
        transform.position = hiddenPosition;
        
    }

    void Update()
    {
        
        if( target.position.x > transform.position.x - activationDistance && target.position.x < transform.position.x + activationDistance )
        {
            StartCoroutine( TextAppear() );
        }
        else
        {
            StartCoroutine( TextDisappear() );
        }

    }

    IEnumerator TextAppear()
    {

        while( textColor.a < 1f && transform.position.y < originalPosition.y - 0.01f )
        {

            textColor.a += Time.deltaTime;
            transform.position = Vector3.Lerp( transform.position, originalPosition, Time.deltaTime);

            this.GetComponent<TMP_Text>().color = textColor;

            yield return new WaitForSeconds(0.01f);

        }

    }

    IEnumerator TextDisappear()
    {

        while( textColor.a > 0f && transform.position.y > hiddenPosition.y + 0.01f )
        {

            textColor.a -= Time.deltaTime;
            transform.position = Vector3.Lerp( transform.position, hiddenPosition, Time.deltaTime);

            this.GetComponent<TMP_Text>().color = textColor;

            yield return new WaitForSeconds(0.01f);

        }

    }

}