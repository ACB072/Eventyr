using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Renderizador de sprites
    public Light shine;
    private YsbridManager Ysbrid;

    [HideInInspector]
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        shine.enabled = false;
        isActive = false;
        Ysbrid = YsbridManager.ysbridInstance;
    }

    // Update is called once per frame
    void Update()
    {
        // Si el personaje principal entra en el radio de influencia del altar
        if ((Ysbrid.transform.position - transform.position).sqrMagnitude < 1.7 * 1.7)
        {
            // Si pulsa la tecla F
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Si tienes alguna de las dos gemas
                if (Ysbrid.hasBlueGem && Ysbrid.hasRedGem)
                {
                    shine.enabled = true;
                    isActive = true;
                }
                /*else
                    StartCoroutine(NoGemMsg()); // Despliega un mensaje*/
            }

        }
    }
}
