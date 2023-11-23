using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    private YsbridManager Ysbrid;
    private DewinManager Dewin;
    private CladdwydManager Cladd;

    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;

        if (DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if (CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;
    }

    void Update()
    {
        if ((Ysbrid.transform.position - transform.position).sqrMagnitude < 6 * 6)
        {
            if (Input.GetKeyDown(KeyCode.E))
                OpenShop();
        }
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Buy(string article)
    {
        switch(article)
        {
            case "TonicoVida":

                if (Ysbrid.coins >= 5)
                {
                    if(Ysbrid.health < Ysbrid.healthBar.maxValue)
                    {
                        Ysbrid.coins -= 5;
                        SumaVida(15);
                    }
                    else
                        Debug.Log("Vida al máximo");
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;

            case "TonicoMana":

                if (Ysbrid.coins >= 5)
                {
                    if (Ysbrid.mana < Ysbrid.manaBar.maxValue)
                    {
                        Ysbrid.coins -= 5;
                        SumaMana(15);
                    }
                    else
                        Debug.Log("Mana al máximo");
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;

            case "PocionVida":

                if (Ysbrid.coins >= 10)
                {
                    if (Ysbrid.health < Ysbrid.healthBar.maxValue)
                    {
                        Ysbrid.coins -= 10;
                        SumaVida(25);
                    }
                    else
                        Debug.Log("Vida al máximo");
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;

            case "PocionMana":

                if (Ysbrid.coins >= 10)
                {
                    if (Ysbrid.mana < Ysbrid.manaBar.maxValue)
                    {
                        Ysbrid.coins -= 10;
                        SumaMana(25);
                    }
                    else
                        Debug.Log("Mana al máximo");
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;

            case "Anillo":

                if (Ysbrid.coins >= 20)
                {
                    Ysbrid.coins -= 20;
                    Ysbrid.healthBar.maxValue += 10;
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;

            case "Libro":

                if (Ysbrid.coins >= 20)
                {
                    Ysbrid.coins -= 20;
                    Ysbrid.manaBar.maxValue += 10;
                }
                else
                    Debug.Log("No tienes suficientes monedas");

                break;
        }
    }

    void SumaVida(int vidaSumada)
    {
        int vidaYsbrid = Ysbrid.health;

        // Si la vida de Ysbrid más la vida sumada es mayor que el valor máximo de la barra de vida
        if ((vidaYsbrid + vidaSumada) > Ysbrid.healthBar.maxValue)
            Ysbrid.health = (int)Ysbrid.healthBar.maxValue;
        else
            Ysbrid.health += vidaSumada;

        if (Dewin != null)
        {
            int vidaDewin = Dewin.dewinHealth;

            if ((vidaDewin + vidaSumada) > Ysbrid.healthBar.maxValue)
                Dewin.dewinHealth = (int)Ysbrid.healthBar.maxValue;
            else
                Dewin.dewinHealth += vidaSumada;
        }

        if (Cladd != null)
        {
            int vidaCladd = Cladd.claddwydHealth;

            if ((vidaCladd + vidaSumada) > Ysbrid.healthBar.maxValue)
                Cladd.claddwydHealth = (int)Ysbrid.healthBar.maxValue;
            else
                Cladd.claddwydHealth += vidaSumada;
        }
    }

    void SumaMana(int manaSumado)
    {
        int manaYsbrid = Ysbrid.mana;

        // Si el mana de Ysbrid más el mana sumado es mayor que el valor máximo de la barra de mana
        if ((manaYsbrid + manaSumado) > Ysbrid.manaBar.maxValue)
            Ysbrid.mana = (int)Ysbrid.manaBar.maxValue;
        else
            Ysbrid.mana += manaSumado;

        if (Dewin != null)
        {
            int manaDewin = Dewin.dewinMana;

            if ((manaDewin + manaSumado) > Ysbrid.manaBar.maxValue)
                Dewin.dewinMana = (int)Ysbrid.manaBar.maxValue;
            else
                Dewin.dewinMana += manaSumado;
        }

        if (Cladd != null)
        {
            int manaCladd = Cladd.claddwydMana;

            if ((manaCladd + manaSumado) > Ysbrid.manaBar.maxValue)
                Cladd.claddwydMana = (int)Ysbrid.manaBar.maxValue;
            else
                Cladd.claddwydMana += manaSumado;
        }
    }
}
