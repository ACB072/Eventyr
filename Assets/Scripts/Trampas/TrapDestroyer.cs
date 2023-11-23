using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDestroyer : MonoBehaviour
{
    // Al colisionar una trampa con este objeto esta se destruirá
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
            Destroy(collision.gameObject);
    }
}
