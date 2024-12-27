using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateJudgment : MonoBehaviour
{

    [SerializeField] PlayerMove receivedValue;

    public bool contactEnemy;

    void OnTriggerEnter2D( Collider2D collision )
    {

        if(collision.gameObject.tag == "Enemy" )
        {
            contactEnemy = true;
        }

    }
}
