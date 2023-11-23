using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YsbridFightManager : MonoBehaviour
{
    public Slider fightHealthBar, fightManaBar; // Barras de salud y mana en el HUD

    // Start is called before the first frame update
    void Start()
    {
        fightHealthBar.maxValue = YsbridManager.ysbridInstance.healthBar.maxValue;
        fightManaBar.maxValue = YsbridManager.ysbridInstance.manaBar.maxValue;
        fightHealthBar.value = YsbridManager.ysbridInstance.health;
        fightManaBar.value = YsbridManager.ysbridInstance.mana;
    }

    // Update is called once per frame
    void Update()
    {
        fightHealthBar.value = YsbridManager.ysbridInstance.health;
        fightManaBar.value = YsbridManager.ysbridInstance.mana;
    }
}
