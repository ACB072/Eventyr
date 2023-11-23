using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DewinFightManager : MonoBehaviour
{
    public Slider fightHealthBar, fightManaBar; // Barras de salud y mana en el HUD

    // Start is called before the first frame update
    void Start()
    {
        fightHealthBar.maxValue = YsbridManager.ysbridInstance.healthBar.maxValue;
        fightManaBar.maxValue = YsbridManager.ysbridInstance.manaBar.maxValue;
        fightHealthBar.value = DewinManager.dewinInstance.dewinHealth;
        fightManaBar.value = DewinManager.dewinInstance.dewinMana;
    }

    // Update is called once per frame
    void Update()
    {
        fightHealthBar.value = DewinManager.dewinInstance.dewinHealth;
        fightManaBar.value = DewinManager.dewinInstance.dewinMana;
    }

    public int DewinPhysicAttack()
    {
        int standardDamage = 5;
        return standardDamage + DewinManager.dewinInstance.dewinPhysic;
    }

    public int DewinMagicAttack()
    {
        int standardDamage = 15;
        return standardDamage + DewinManager.dewinInstance.dewinMagic;
    }
}
