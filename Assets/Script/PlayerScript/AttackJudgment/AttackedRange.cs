using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedRange : MonoBehaviour
{

    public bool dashAttackActivator;

    SpriteRenderer spriteRenderer;
    Color color;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if( dashAttackActivator )
        {
            color.a = Mathf.Lerp( color.a, 1f, 30f * Time.deltaTime );
            spriteRenderer.GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            color.a = 0.2f;
            spriteRenderer.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void OnTriggerStay2D( Collider2D collision )
    {
        
        if( collision.gameObject.tag == "EnemyAttack" )
        {
            dashAttackActivator = true;
        }

    }
}
