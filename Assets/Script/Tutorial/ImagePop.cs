using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePop : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float activationDistance;

    Vector3 originalPosition;
    Vector3 hiddenPosition;

    Color ImageColor;

    void Start()
    {
        ImageColor = this.GetComponent<SpriteRenderer>().color;
        originalPosition = transform.position;
        hiddenPosition = new Vector3( transform.position.x, transform.position.y -1f, transform.position.z );
        transform.position = hiddenPosition;
        
    }

    void Update()
    {
        
        if( target.position.x > transform.position.x - activationDistance && target.position.x < transform.position.x + activationDistance )
        {
            StartCoroutine( ImageAppear() );
        }
        else
        {
            StartCoroutine( ImageDisappear() );
        }

    }

    IEnumerator ImageAppear()
    {


        while( ImageColor.a < 1f && transform.position.y < originalPosition.y - 0.01f )
        {

            
            ImageColor.a += Time.deltaTime;
            transform.position = Vector3.Lerp( transform.position, originalPosition, Time.deltaTime);

            this.GetComponent<SpriteRenderer>().color = ImageColor;

            yield return new WaitForSeconds(0.01f);

        }

    }

    IEnumerator ImageDisappear()
    {

        while( ImageColor.a > 0f && transform.position.y > hiddenPosition.y + 0.01f )
        {

            ImageColor.a -= Time.deltaTime;
            transform.position = Vector3.Lerp( transform.position, hiddenPosition, Time.deltaTime);

            this.GetComponent<SpriteRenderer>().color = ImageColor;

            yield return new WaitForSeconds(0.01f);

        }

    }

}