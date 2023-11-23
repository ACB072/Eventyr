using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public int resistance;                  // Resistencia del objeto
    public SpriteRenderer spriteRenderer;   // Renderizador de sprites
    public Sprite broken;                   // Sprite del objeto roto
    public AudioSource audioSource;         // Fuente de audio
    public AudioClip hit;                   // Sonido del objeto golpeado
    public AudioClip brokenSound;           // Sonido del objeto roto

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Espada" || collision.gameObject.tag == "Espada1" ||
            collision.gameObject.tag == "Espada2" || collision.gameObject.tag == "Espada3")
            StartCoroutine(ObstacleDestroyer());
    }

    IEnumerator ObstacleDestroyer()
    {
        audioSource.clip = hit;
        audioSource.Play();
        resistance--;
        if(resistance == 0)
        {
            audioSource.clip = brokenSound;
            audioSource.Play();
            spriteRenderer.sprite = broken;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
