using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackCollider : MonoBehaviour
{

    TrailRenderer trail;
    EdgeCollider2D collider;

    void Start()
    {

        trail = GetComponent<TrailRenderer>();
        collider = GetComponent<EdgeCollider2D>();

    }

    void Update()
    {
    
        List<Vector2> points = new List<Vector2>();

        for( int position = 0; position < trail.positionCount; position ++ )
        {
            points.Add( trail.GetPosition( position ) - transform.position );
        }
        
        collider.SetPoints( points );
        
    }

}