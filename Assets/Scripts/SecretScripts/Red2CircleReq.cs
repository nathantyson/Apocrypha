using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red2CircleReq : MonoBehaviour
{
    Rigidbody2D bod;
    public bool frozen;
    CapsuleCollider2D capsule;

    void Start()
    {
        bod = GetComponentInParent<Rigidbody2D>();
        frozen = false;
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "planet2REQ")
        {

            //bod.simulated = false;
            bod.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            frozen = true;
            capsule.enabled = false;

        }
    }

}
