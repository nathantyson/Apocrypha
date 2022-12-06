using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircleReq : MonoBehaviour
{
    Rigidbody2D bod;
    public bool frozen;
    CapsuleCollider2D capsule;
    void Start()
    {
        frozen = false;
        bod = GetComponentInParent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "planet1REQ")
        {
            bod.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            frozen = true;
            capsule.enabled = false;
            //bod.simulated = false;

        }
    }


}
