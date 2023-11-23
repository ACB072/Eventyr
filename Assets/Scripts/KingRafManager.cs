using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRafManager : MonoBehaviour
{
    private YsbridManager Ysbrid;

    // Start is called before the first frame update
    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;

        gameObject.SetActive(false);

        if(Ysbrid.isDevilCladdDead == true)
            gameObject.SetActive(true);
    }
}
