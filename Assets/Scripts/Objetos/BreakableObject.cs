using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject[] items;              // Array de items
    private Vector3 itemSpawner;            // Lugar donde aparecerá el item
    public SpriteRenderer spriteRenderer;   // Renderizador de sprites
    public Sprite broken;                   // Sprite de objeto roto
    public AudioSource audioSource;         // Fuente de sonido
    public AudioClip brokenSound;           // Efecto de sonido
    public bool isBroken = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBroken)
        {
            // Si el objeto colisiona con la espada del personaje
            if (collision.gameObject.tag == "Espada")
                StartCoroutine(BrokenObject()); // Se rompe el objeto
            else if (collision.gameObject.tag == "Espada1")
                StartCoroutine(BrokenObject()); // Se rompe el objeto
            else if (collision.gameObject.tag == "Espada2")
                StartCoroutine(BrokenObject()); // Se rompe el objeto
            else if (collision.gameObject.tag == "Espada3")
                StartCoroutine(BrokenObject()); // Se rompe el objeto
        }
        else
            Destroy(gameObject);
    }

    IEnumerator BrokenObject()
    {
        isBroken = true;

        // Establece la posicion del item (la misma que la del objeto)
        itemSpawner = new Vector3(transform.position.x, transform.position.y, -1);

        // Establece el sprite del objeto roto
        spriteRenderer.sprite = broken;

        // Establece el efecto de sonido
        audioSource.clip = brokenSound;
        audioSource.Play();

        // Espera por 1 segundo
        yield return new WaitForSeconds(1.2f);

        // Destruye el objeto
        Destroy(gameObject);

        // Lanza una probabilidad para los items
        Probability();
    }

    void Probability()
    {
        if (gameObject.tag == "Cristal de Fuego" || gameObject.tag == "Cristal de Hielo")
        {
            int probability = Random.Range(0, 50);

            if (probability >= 40)
                Instantiate(items[0], itemSpawner, Quaternion.identity);
        }
        else
        {
            int probability = Random.Range(0, 100);

            if (probability >= 45 && probability <= 74) // Entran: Monedas
                Instantiate(items[0], itemSpawner, Quaternion.identity);

            if (probability >= 75 && probability <= 90) // Entran: Tonico de Vida y Tonico de Mana
            {
                int tonicItem = Random.Range(1, 3);
                Instantiate(items[tonicItem], itemSpawner, Quaternion.identity);
            }

            if (probability >= 91 && probability <= 100) // Entran: Llaves
                Instantiate(items[3], itemSpawner, Quaternion.identity);
        }
    }
}
