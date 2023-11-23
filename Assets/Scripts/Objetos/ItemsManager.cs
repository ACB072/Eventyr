
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Renderizador de sprites
    public AudioSource audioSource;        // Fuente de sonido
    public AudioClip soundEffect;          // Efecto de sonido
    private YsbridManager Ysbrid;          // Objeto Ysbrid
    private DewinManager Dewin;            // Objeto Dewin
    private CladdwydManager Cladd;         // Objeto Claddwyd

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Ysbrid = YsbridManager.ysbridInstance;

        if(DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if(CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el item colisiona con el personaje principal
        if(collision.gameObject.tag == "Ysbrid")
        {
            switch(gameObject.tag)
            {
                case "Tonico Vida":
                    StartCoroutine(PickerManager());
                    SumaVida(15);
                    break;

                case "Tonico Mana":
                    StartCoroutine(PickerManager());
                    SumaMana(15);
                    break;

                case "Moneda":
                    StartCoroutine(PickerManager());
                    Ysbrid.coins++;
                    break;

                case "Pocion Vida":
                    StartCoroutine(PickerManager());
                    SumaVida(25);
                    break;

                case "Pocion Mana":
                    StartCoroutine(PickerManager());
                    SumaMana(25);
                    break;

                case "Anillo Vida":
                    StartCoroutine(PickerManager());
                    Ysbrid.healthBar.maxValue += 10;
                    break;

                case "Libro":
                    StartCoroutine(PickerManager());
                    Ysbrid.manaBar.maxValue += 10;
                    break;

                case "Llave":
                    StartCoroutine(PickerManager());
                    Ysbrid.keys++;
                    break;

                case "Gema Roja":
                    StartCoroutine(PickerManager());
                    Ysbrid.hasRedGem = true;
                    break;

                case "Gema Azul":
                    StartCoroutine(PickerManager());
                    Ysbrid.hasBlueGem = true;
                    break;

                case "EspadaItem":
                    StartCoroutine(PickerManager());
                    Ysbrid.hasSword = true;
                    break;

                case "AntorchaItem":
                    StartCoroutine(PickerManager());
                    Ysbrid.torch.SetActive(true);
                    Ysbrid.hasTorch = true;
                    break;
            }
        }
    }

    void SumaVida(int vidaSumada)
    {
        int vidaYsbrid = Ysbrid.health;

        // Si la vida de Ysbrid más la vida sumada es mayor que el valor máximo de la barra de vida
        if ((vidaYsbrid + vidaSumada) > Ysbrid.healthBar.maxValue)
            Ysbrid.health = (int) Ysbrid.healthBar.maxValue;
        else
            Ysbrid.health += vidaSumada;

        if(Dewin != null)
        {
            int vidaDewin = Dewin.dewinHealth;

            if ((vidaDewin + vidaSumada) > Ysbrid.healthBar.maxValue)
                Dewin.dewinHealth = (int) Ysbrid.healthBar.maxValue;
            else
                Dewin.dewinHealth += vidaSumada;
        }

        if(Cladd != null)
        {
            int vidaCladd = Cladd.claddwydHealth;

            if ((vidaCladd + vidaSumada) > Ysbrid.healthBar.maxValue)
                Cladd.claddwydHealth = (int) Ysbrid.healthBar.maxValue;
            else
                Cladd.claddwydHealth += vidaSumada;
        }
    }

    void SumaMana(int manaSumado)
    {
        int manaYsbrid = Ysbrid.mana;

        // Si el mana de Ysbrid más el mana sumado es mayor que el valor máximo de la barra de mana
        if ((manaYsbrid + manaSumado) > Ysbrid.manaBar.maxValue)
            Ysbrid.mana = (int) Ysbrid.manaBar.maxValue;
        else
            Ysbrid.mana += manaSumado;

        if (Dewin != null)
        {
            int manaDewin = Dewin.dewinMana;

            if ((manaDewin + manaSumado) > Ysbrid.manaBar.maxValue)
                Dewin.dewinMana = (int) Ysbrid.manaBar.maxValue;
            else
                Dewin.dewinMana += manaSumado;
        }

        if (Cladd != null)
        {
            int manaCladd = Cladd.claddwydMana;

            if ((manaCladd + manaSumado) > Ysbrid.manaBar.maxValue)
                Cladd.claddwydMana = (int) Ysbrid.manaBar.maxValue;
            else
                Cladd.claddwydMana += manaSumado;
        }
    }

    IEnumerator PickerManager()
    {
        audioSource.clip = soundEffect;
        audioSource.Play();
        spriteRenderer.enabled = false;
        if(gameObject.tag == "Gema Roja" || gameObject.tag == "Gema Azul" || gameObject.tag == "EspadaItem" || gameObject.tag == "AntorchaItem")
            yield return new WaitForSeconds(2);
        else if(gameObject.tag == "Libro")
            yield return new WaitForSeconds(0.5f);
        else
            yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
