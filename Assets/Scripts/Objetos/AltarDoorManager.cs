using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarDoorManager : MonoBehaviour
{
    public BoxCollider2D door;                  // Collider
    public AltarManager blueAltar;              // Altar Azul
    public AltarManager redAltar;               // Altar Rojo
    public Light pathLight;                     // Haz de luz
    public SpriteRenderer spriteRenderer;       // Renderizador de sprites
    public Sprite openSprite;                   // Sprite de la puerta abierta
    public AudioSource audioSource;             // Fuente de audio
    public AudioClip openSound;                 // Sonido de la puerta abierta

    void Start()
    {
        door.isTrigger = false;
        pathLight.enabled = false;
    }

    void Update()
    {
       if(blueAltar.isActive && redAltar.isActive)
            StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3);
        pathLight.enabled = true;
        audioSource.clip = openSound;
        audioSource.Play();
        spriteRenderer.sprite = openSprite;
        door.isTrigger = true;
    }
}
