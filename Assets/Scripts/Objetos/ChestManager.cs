using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject[] items;              // Array de objetos item
    private Vector3 itemSpawner;            // Donde aparecerá el item
    public SpriteRenderer spriteRenderer;   // Renderizador de sprites
    public Sprite open;                     // Sprite cofre abierto
    public AudioSource audioSource;
    public AudioClip openSound;
    private Animator noKeyAnim;
    private bool isClosed;

    private YsbridManager Ysbrid;

    void Start()
    {
        isClosed = true;
        Ysbrid = YsbridManager.ysbridInstance;

        if (noKeyAnim == null)
            noKeyAnim = Ysbrid.noKeyAnimator;
    }

    void Update()
    {
        // Si el personaje principal entra en el radio de influencia del cofre
        if ((Ysbrid.transform.position - transform.position).sqrMagnitude < 1.5 * 1.5)
        {
            if(isClosed)
            {
                // Si se pulsa la tecla E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Si tiene más de 0 llaves
                    if (Ysbrid.keys > 0)
                    {
                        StartCoroutine(OpenChest()); // Abre el cofre
                        isClosed = false;
                    }
                    else
                        StartCoroutine(NoKeyAnimation());
                        
                }
            }               
        }
    }

    IEnumerator NoKeyAnimation()
    {
        noKeyAnim.SetBool("NoKey", true);
        yield return new WaitForSeconds(1.5f);
        noKeyAnim.SetBool("NoKey", false);
    }

    IEnumerator OpenChest()
    {
        // Establece la posicion del item (la misma que la del cofre)
        itemSpawner = new Vector3(transform.position.x, transform.position.y, -1);

        // Resta una llave
        Ysbrid.keys--;

        // Se reproduce el sonido al abrir el cofre
        audioSource.clip = openSound;
        audioSource.Play();

        // Establece el sprite del cofre abierto
        spriteRenderer.sprite = open;

        // Espera por dos segundos
        yield return new WaitForSeconds(2);

        // Destruye el objeto cofre
        Destroy(gameObject);

        // Lanza una probabilidad para los items
        Probability();
    }

    void Probability()
    {
        int probability = Random.Range(0, 100);

        if (probability >= 0 && probability <= 79) // Entran: Pocion de Vida y Pocion de Mana
        {
            int rareItem = Random.Range(0, 2);
            Instantiate(items[rareItem], itemSpawner, Quaternion.identity);
        }

        if (probability >= 80 && probability <= 100) // Entran: Anillo y Libro
        {
            int epicItem = Random.Range(2, 4);
            Instantiate(items[epicItem], itemSpawner, Quaternion.identity);
        }
    }
}
