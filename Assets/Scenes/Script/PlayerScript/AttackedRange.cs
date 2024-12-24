using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedRange : MonoBehaviour
{

    public bool dashAttackActivator;

    void OnTriggerEnter2D( Collider2D collision )
    {
        
        if( collision.gameObject.tag == "EnemyAttack" )
        {
            dashAttackActivator = true;
        }

    }
}
