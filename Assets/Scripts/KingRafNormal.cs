using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRafNormal : MonoBehaviour
{
    private YsbridManager Ysbrid;

    // Start is called before the first frame update
    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;

        if (Ysbrid.isDevilCladdDead == true)
            gameObject.SetActive(false);
    }
}
