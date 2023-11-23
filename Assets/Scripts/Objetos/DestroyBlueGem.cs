using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlueGem : MonoBehaviour
{
    private YsbridManager Ysbrid;

    // Start is called before the first frame update
    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;

        if (Ysbrid.hasBlueGem)
            gameObject.SetActive(false);
    }
}
