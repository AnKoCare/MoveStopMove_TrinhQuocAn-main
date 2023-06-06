using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : GameUnit
{
    [SerializeField] private Rigidbody rb;
    
    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }    
    }
}
