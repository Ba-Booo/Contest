using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public float bullatSpeed;
    GameObject target;
    Rigidbody2D bullatRB;

    void Start()
    {     
        
        bullatRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        Vector2 moveDir = (target.transform.position - transform.position).normalized * bullatSpeed;
        bullatRB.velocity = moveDir;

        Destroy(this.gameObject, 2);

    }

}
