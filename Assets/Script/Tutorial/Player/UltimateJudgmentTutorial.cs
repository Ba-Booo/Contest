using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateJudgmentTutorial : MonoBehaviour
{

    [SerializeField] PlayerMoveTutorial receivedValue;

    public bool contactEnemy;

    void OnTriggerEnter2D( Collider2D collision )
    {

        if(collision.gameObject.tag == "Enemy" )
        {
            contactEnemy = true;
            receivedValue.enemyCount += 1;
        }

    }

}
