using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    public Light redLight;  // Luz roja

    void Start()
    {
        redLight.enabled = false;    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ysbrid")
            redLight.enabled = true;
    }
}
