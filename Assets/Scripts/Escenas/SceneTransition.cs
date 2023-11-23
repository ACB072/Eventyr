using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public int index;                   // Indice de la escena a cargar
    public Image image;                 // Imagen para el fade
    public Animator anim;               // Animación de la imagen
    public AudioSource audioSource;     // Fuente de sonido
    public AudioClip enterSound;        // Sonido al entrar por una puerta
    private YsbridManager Ysbrid;       // Objeto Ysbrid
    public float x, y;                  // Posicion donde aparecerá el personaje

    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;    
    }

    // Si el player colisiona con el collider dispara este método
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ysbrid"))
            StartCoroutine(Transition()); // Lanza la transición de la escena
    }

    // Método para el fading
    IEnumerator Transition()
    {
        // Establece la condición del fade a verdadero
        anim.SetBool("Fade", true);

        audioSource.clip = enterSound;
        audioSource.Play();

        // Hasta que la animacion no se establezca en 1 no carga la escena
        yield return new WaitUntil(() => image.color.a == 1);

        // Establece el proximo punto de aparición
        Ysbrid.nextSpawn = new Vector2(x, y);

        // Carga la escena con el indice dado
        SceneManager.LoadScene(index);
    }
}