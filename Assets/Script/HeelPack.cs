using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelPack : MonoBehaviour
{

    [SerializeField] PlayerUI playerHeel;
    AudioSource audioSource;

    [SerializeField] GameObject heelPartical;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D( Collider2D collision )
    {

        if( collision.gameObject.tag == "Player" )
        {
            
            playerHeel.playerNowHP = playerHeel.playerMaxHP;
            Instantiate( heelPartical, transform.position, transform.rotation);
            Destroy(this.gameObject);
        
        }
        
    }

}
