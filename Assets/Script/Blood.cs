using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Blood : MonoBehaviour
{

    [SerializeField] float destroyDelay;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine( BloodSound() );
        Destroy(this.gameObject, destroyDelay);
    }

    void Update()
    {
        audioSource.pitch = Time.timeScale;
    }

    IEnumerator BloodSound()
    {

        yield return new WaitForSeconds( Random.Range( 0f, 0.2f ) );
        
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    
    }
    
}
