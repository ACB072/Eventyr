using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireManager : MonoBehaviour
{
    private Animator anim;              // Animacion de la hoguera
    public AudioSource audioSource;     // Fuente de audio
    public AudioClip litClip;           // Sonido de la hoguera encendida
    public GameObject bonfireLight;     // Luz de la hoguera

    void Start()
    {
        bonfireLight.SetActive(false);
        anim = GetComponent<Animator>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ysbrid")
        {
            anim.SetBool("Lit", true);
            bonfireLight.SetActive(true);
            audioSource.clip = litClip;
            audioSource.Play();
        }
    }
}
