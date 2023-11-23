using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CladdFightManager : MonoBehaviour
{
    public Slider fightHealthBar, fightManaBar; // Barras de salud y mana en el HUD

    // Start is called before the first frame update
    void Start()
    {
        if(CladdwydManager.claddInstance != null)
        {
            fightHealthBar.maxValue = YsbridManager.ysbridInstance.healthBar.maxValue;
            fightManaBar.maxValue = YsbridManager.ysbridInstance.manaBar.maxValue;
            fightHealthBar.value = CladdwydManager.claddInstance.claddwydHealth;
            fightManaBar.value = CladdwydManager.claddInstance.claddwydMana;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CladdwydManager.claddInstance != null)
        {
            fightHealthBar.value = CladdwydManager.claddInstance.claddwydHealth;
            fightManaBar.value = CladdwydManager.claddInstance.claddwydMana;
        }
    }
}
