using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    [SerializeField] float destroyDelay;

    void Start()
    {
        Destroy(this.gameObject, destroyDelay);
    }
    
}
