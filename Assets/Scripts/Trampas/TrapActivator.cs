using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    public GameObject trap;         // Objecto Trampa

    void Start()
    {
        trap.SetActive(false);    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ysbrid")
            trap.SetActive(true);
    }
}
